using System;
using System.Runtime.InteropServices;

namespace DinkeyHelper
{
    public abstract class BaseDongleProtectionCheck
    {
        protected static int MY_SDSN { get; set; }           // !!!! change this value to be the value of your SDSN (demo = 10101)
        protected static string MY_PRODCODE { get; set; }   // !!!! change this value to match the Product Code in the dongle

        // functions - must specify only one
        protected const int PROTECTION_CHECK = 1;      // checks for dongle, check program params...

        protected const int EXECUTE_ALGORITHM = 2;     // protection check + calculate answer for specified algorithm with specified inputs
        protected const int WRITE_DATA_AREA = 3;       // protection check + writes dongle data area
        protected const int READ_DATA_AREA = 4;        // protection check + reads dongle data area
        protected const int ENCRYPT_USER_DATA = 5;     // protection check + the dongle will encrypt user data
        protected const int DECRYPT_USER_DATA = 6;     // protection check + the dongle will decrypt user data
        protected const int FAST_PRESENCE_CHECK = 7;   // checks for the presence of the correct dongle only with minimal security, no flags allowed.

        // flags - can specify as many as you like
        protected const int DEC_ONE_EXEC = 1;          // decrement execs by 1s

        protected const int DEC_MANY_EXECS = 2;        // decrement execs by number specified in execs_decrement
        protected const int START_NET_USER = 4;        // starts a network user
        protected const int STOP_NET_USER = 8;         // stops a network user (a protection check is NOT performed)
        protected const int USE_FUNCTION_ARGUMENT = 16;// use the extra argument in the function for pointers
        protected const int CHECK_LOCAL_FIRST = 32;    // always look in local ports before looking in network ports
        protected const int CHECK_NETWORK_FIRST = 64;  // always look on the network before looking in local ports
        protected const int USE_ALT_LICENCE_NAME = 128;    // use name specified in alt_licence_name instead of the default one
        protected const int DONT_SET_MAXDAYS_EXPIRY = 256; // if the max days expiry date has not been calculated then do not do it this time
        protected const int MATCH_DONGLE_NUMBER = 512;     // restrict the search to match the dongle number specified in the DRIS

        public virtual bool WriteData(byte[] dataToWrite, int dataOffset)
        {
            try
            {
                int ret_code;
                var dris = new DRIS();                         // initialise the DRIS with random values & set the header

                dris.size = Marshal.SizeOf(dris);
                dris.function = WRITE_DATA_AREA;                // standard protection check & write data to dongle
                dris.flags = USE_FUNCTION_ARGUMENT;             // you have to do it like this in C#
                dris.rw_offset = dataOffset;
                dris.rw_length = dataToWrite.GetLength(0);

                ret_code = DinkeyPro.DDProtCheck(dris, dataToWrite);

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

        public virtual byte[] ReadData(int dataLength, int dataOffset)
        {
            try
            {
                int ret_code;
                var dris = new DRIS();                         // initialise the DRIS with random values & set the header
                var dataToRead = new byte[dataLength];

                dris.size = Marshal.SizeOf(dris);
                dris.function = READ_DATA_AREA;                 // standard protection check & read data
                dris.flags = USE_FUNCTION_ARGUMENT;             // you have to do it like this in C#
                dris.rw_offset = dataOffset;
                dris.rw_length = dataToRead.GetLength(0);

                ret_code = DinkeyPro.DDProtCheck(dris, dataToRead);

                if (ret_code != 0)
                {
                    throw new Exception(dris.DisplayError(ret_code, dris.ext_err));
                }

                return dataToRead;
            }
            catch
            {
                throw;
            }
        }

        public virtual bool EncryptData(byte[] data)
        {
            try
            {
                int ret_code;
                var dris = new DRIS();                         // initialise the DRIS with random values & set the header

                dris.size = Marshal.SizeOf(dris);
                dris.function = ENCRYPT_USER_DATA;              // standard protection check & encrypt data
                dris.flags = USE_FUNCTION_ARGUMENT;
                dris.rw_length = data.GetLength(0);
                dris.data_crypt_key_num = 1;

                ret_code = DinkeyPro.DDProtCheck(dris, data);

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

        public virtual byte[] DecryptData(byte[] data)
        {
            try
            {
                int ret_code;
                var dris = new DRIS();

                dris.size = Marshal.SizeOf(dris);
                dris.function = DECRYPT_USER_DATA;              // standard protection check & read data
                dris.flags = USE_FUNCTION_ARGUMENT;             // you have to do it like this in C#
                dris.rw_length = data.GetLength(0);
                dris.data_crypt_key_num = 1;

                ret_code = DinkeyPro.DDProtCheck(dris, data);

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

        public virtual byte[] GetEncryptedData(byte[] data)
        {
            try
            {
                int ret_code;
                var dris = new DRIS();                         // initialise the DRIS with random values & set the header

                dris.size = Marshal.SizeOf(dris);
                dris.function = ENCRYPT_USER_DATA;              // standard protection check & encrypt data
                dris.flags = USE_FUNCTION_ARGUMENT;
                dris.rw_length = data.GetLength(0);
                dris.data_crypt_key_num = 1;

                ret_code = DinkeyPro.DDProtCheck(dris, data);

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

        public virtual bool CheckProtection()
        {
            try
            {
                int ret_code;
                var dris = new DRIS();                         // initialise the DRIS with random values & set the header

                dris.size = Marshal.SizeOf(dris);
                dris.function = PROTECTION_CHECK;               // standard protection check
                dris.flags = 0;                                 // no extra flags, but you may want to specify some if you want to start a network user or decrement execs,...

                ret_code = DinkeyPro.DDProtCheck(dris, null);

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
    }
}