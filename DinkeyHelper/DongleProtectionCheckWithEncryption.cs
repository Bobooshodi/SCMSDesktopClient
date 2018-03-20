using System;
using System.Runtime.InteropServices;

namespace DinkeyHelper
{
    public abstract class DongleProtectionCheckWithEncryption : DongleProtectionCheckWithAlgorithm
    {
        public abstract void CryptDRIS(DRIS dris);

        public abstract void CryptApiData(DRIS dris, byte[] data, int length, int alg_answer);

        public override bool WriteData(byte[] dataToWrite, int dataOffset)
        {
            try
            {
                int ret_code, alg_ans;
                var dris = new DRIS();                         // initialise the DRIS with random values & set the header

                dris.size = Marshal.SizeOf(dris);
                dris.function = WRITE_DATA_AREA;                // standard protection check & write data to dongle
                dris.flags = USE_FUNCTION_ARGUMENT;             // you have to do it like this in C#
                dris.rw_offset = dataOffset;
                dris.rw_length = dataToWrite.GetLength(0);

                alg_ans = AlgorithmComputation();

                // encrypt data we want to write.
                CryptApiData(dris, dataToWrite, dataToWrite.GetLength(0), alg_ans);

                CryptDRIS(dris);                                // encrypt DRIS (!!!!you should separate from DDProtCheck for greater security)

                ret_code = DinkeyPro.DDProtCheck(dris, dataToWrite);

                CryptDRIS(dris);                                // decrypt DRIS (!!!!you should separate from DDProtCheck for greater security)

                if (ret_code != 0)
                {
                    throw new Exception(dris.DisplayError(ret_code, dris.ext_err));
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        public override byte[] ReadData(int dataLength, int dataOffset)
        {
            int ret_code, alg_ans;
            var dris = new DRIS();                         // initialise the DRIS with random values & set the header
            var dataRead = new byte[dataLength];

            dris.size = Marshal.SizeOf(dris);
            dris.function = READ_DATA_AREA;                 // standard protection check & read data
            dris.flags = USE_FUNCTION_ARGUMENT;             // you have to do it like this in C#
            dris.rw_offset = dataOffset;
            dris.rw_length = dataRead.GetLength(0);

            // calculate r/w algorithm answer - NB need to replace MyRWAlgorithm code with code for r/w algorithm
            alg_ans = AlgorithmComputation();

            CryptDRIS(dris);                                // encrypt DRIS (!!!!you should separate from DDProtCheck for greater security)

            ret_code = DinkeyPro.DDProtCheck(dris, dataRead);

            CryptDRIS(dris);                                // decrypt DRIS (!!!!you should separate from DDProtCheck for greater security)

            if (ret_code != 0)
            {
                throw new Exception(dris.DisplayError(ret_code, dris.ext_err));
            }

            // decrypt data that was read.
            CryptApiData(dris, dataRead, dataRead.GetLength(0), alg_ans);

            return dataRead;
        }

        public override bool EncryptData(byte[] data)
        {
            try
            {
                int ret_code, alg_ans;
                var dris = new DRIS();                         // initialise the DRIS with random values & set the header

                dris.size = Marshal.SizeOf(dris);
                dris.function = ENCRYPT_USER_DATA;              // standard protection check & encrypt data
                dris.flags = USE_FUNCTION_ARGUMENT;
                dris.rw_length = data.GetLength(0);
                dris.data_crypt_key_num = 1;

                alg_ans = AlgorithmComputation();

                // encrypt data we pass to our API.
                CryptApiData(dris, data, data.GetLength(0), alg_ans);

                CryptDRIS(dris);                                // encrypt DRIS (!!!!you should separate from DDProtCheck for greater security)

                ret_code = DinkeyPro.DDProtCheck(dris, data);

                CryptDRIS(dris);                                // decrypt DRIS (!!!!you should separate from DDProtCheck for greater security)

                if (ret_code != 0)
                {
                    throw new Exception(dris.DisplayError(ret_code, dris.ext_err));
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        public override byte[] GetEncryptedData(byte[] data)
        {
            try
            {
                int ret_code, alg_ans;
                var dris = new DRIS();                         // initialise the DRIS with random values & set the header

                dris.size = Marshal.SizeOf(dris);
                dris.function = ENCRYPT_USER_DATA;              // standard protection check & encrypt data
                dris.flags = USE_FUNCTION_ARGUMENT;
                dris.rw_length = data.GetLength(0);
                dris.data_crypt_key_num = 1;

                alg_ans = AlgorithmComputation();

                // encrypt data we pass to our API.
                CryptApiData(dris, data, data.GetLength(0), alg_ans);

                CryptDRIS(dris);                                // encrypt DRIS (!!!!you should separate from DDProtCheck for greater security)

                ret_code = DinkeyPro.DDProtCheck(dris, data);

                CryptDRIS(dris);                                // decrypt DRIS (!!!!you should separate from DDProtCheck for greater security)

                if (ret_code != 0)
                {
                    throw new Exception(dris.DisplayError(ret_code, dris.ext_err));
                }

                return data;
            }
            catch
            {
                throw;
            }
        }

        public override byte[] DecryptData(byte[] data)
        {
            try
            {
                int ret_code, alg_ans;
                var dris = new DRIS();

                dris.size = Marshal.SizeOf(dris);
                dris.function = DECRYPT_USER_DATA;             // standard protection check & read data
                dris.flags = USE_FUNCTION_ARGUMENT;            // you have to do it like this in C#
                dris.rw_length = data.GetLength(0);
                dris.data_crypt_key_num = 1;

                CryptDRIS(dris);                               // encrypt DRIS (!!!!you should separate from DDProtCheck for greater security)

                ret_code = DinkeyPro.DDProtCheck(dris, data);

                CryptDRIS(dris);                               // decrypt DRIS (!!!!you should separate from DDProtCheck for greater security)

                if (ret_code != 0)
                {
                    throw new Exception(dris.DisplayError(ret_code, dris.ext_err));
                }

                alg_ans = AlgorithmComputation();
                // decrypt data that was passed to us by the API.
                CryptApiData(dris, data, data.GetLength(0), alg_ans);

                return data;
            }
            catch
            {
                throw;
            }
        }

        public override bool CheckProtection(int flags = 0, int alg_num = 1)
        {
            try
            {
                int ret_code;
                var dris = new DRIS();                     // initialise the DRIS with random values & set the header

                dris.size = Marshal.SizeOf(dris);
                dris.function = EXECUTE_ALGORITHM;          // standard protection check & execute algorithm
                dris.flags = flags;                             // no extra flags, but you may want to specify some if you want to start a network user or decrement execs,...
                dris.alg_number = alg_num;                        // execute algorithm 1 (you do not need to specify this field if you are only using Lite models).
                                                                  // you should remove these entries if you are using Dinkey Lite so that algorithm arguments are random
                dris.var_a = dris_alg_val_a;
                dris.var_b = dris_alg_val_b;
                dris.var_c = dris_alg_val_c;
                dris.var_d = dris_alg_val_d;
                dris.var_e = dris_alg_val_e;
                dris.var_f = dris_alg_val_f;
                dris.var_g = dris_alg_val_g;
                dris.var_h = dris_alg_val_h;

                CryptDRIS(dris);                            // encrypt DRIS (!!!!you should separate from DDProtCheck for greater security)

                ret_code = DinkeyPro.DDProtCheck(dris, null);

                CryptDRIS(dris);                            // decrypt DRIS (!!!!you should separate from DDProtCheck for greater security)

                if (ret_code != 0)
                {
                    throw new Exception(dris.DisplayError(ret_code, dris.ext_err));
                }

                // later in your code you can check other values in the DRIS...
                if (dris.sdsn != MY_SDSN)
                {
                    throw new Exception("Incorrect SDSN! Please modify your source code so that MY_SDSN is set to be your SDSN.");
                }

                // later on in your program you can check the return code again
                if (dris.ret_code != 0)
                {
                    throw new Exception("Dinkey Dongle protection error");
                }

                // if you want you can check the algorithm answer - however if algorithm is from your code then best to just use the answer in your code
                // !!!! Make sure you have replaced the MyAlgorithm routine with the algorithm you have specified in DinkeyAdd
                if (AlgorithmComputation() != dris.alg_answer)
                {
                    throw new Exception("Dinkey protection error!\nYou have not patched your algorithm in the MyAlgorithm routine");
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}