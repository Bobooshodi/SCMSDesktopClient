using System;
using System.Runtime.InteropServices;
using System.Text;
using DinkeyHelper;
using Newtonsoft.Json;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;

namespace SCMSClient.Services.Implementation
{
    public class DinkeyDongleService : DongleProtectionCheckWithEncryption, IDinkeyDongleService
    {
        protected DinkeyDongleService()
        {
            MY_PRODCODE = "SHCSCMS";
            MY_SDSN = 12630;
        }

        protected override Func<int> AlgorithmComputation => () => dris_alg_val_c * dris_alg_val_g *
                                                                   ((dris_alg_val_a + dris_alg_val_b) &
                                                                    dris_alg_val_b) * dris_alg_val_c /
                                                                   ((dris_alg_val_h + dris_alg_val_f) * dris_alg_val_a /
                                                                    dris_alg_val_f);

        public DongleData GetDongleData()
        {
            double onekb = 1024;
            var dataToRead = new byte[4987];
            double totalDataLength = dataToRead.Length;

            var dataLength = onekb;
            var count = Math.Ceiling(dataToRead.Length / onekb);
            var counter = 1;
            var offset = 0;
            while (counter <= count)
            {
                var dataBlockRead = ReadData((int) dataLength, offset);
                Array.Copy(dataBlockRead, 0, dataToRead, offset, (int) dataLength);

                if (totalDataLength - onekb * counter < onekb)
                    dataLength = totalDataLength - onekb * counter;

                offset = (int) (onekb * counter);
                counter++;
            }
            return JsonConvert.DeserializeObject<DongleData>(Encoding.ASCII.GetString(dataToRead));
        }

        public bool IsDonglePresent()
        {
            return CheckProtection();
        }

        public bool WriteDongleData(DongleData data)
        {
            var DataToWrite = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data));
            var status = false;
            const double onekb = 1024;
            double totalDataLength = DataToWrite.Length;
            if (DataToWrite.Length > onekb)
            {
                var dataLength = onekb;
                var count = Math.Ceiling(DataToWrite.Length / onekb);

                var dataWritten = false;
                var counter = 1;
                var offset = 0;
                while (counter <= count)
                {
                    var dataBlockToWrite = new byte[Convert.ToInt64(dataLength)];
                    Array.Copy(DataToWrite, offset, dataBlockToWrite, 0, Convert.ToInt64(dataLength));

                    dataWritten = WriteData(dataBlockToWrite, offset);

                    status |= dataWritten;

                    if (totalDataLength - onekb * counter < onekb)
                        dataLength = totalDataLength - onekb * counter;

                    offset = (int) (onekb * counter);
                    counter++;
                }

                status |= dataWritten;
            }
            else
            {
                status |= WriteData(DataToWrite, 0);
            }
            return status;
        }

        public override void CryptApiData(DRIS dris, byte[] data, int length, int alg_answer)
        {
            int i, j, k;
            var bigseed = new byte[256];
            var S = new byte[256];
            byte temp, t;

            for (i = 0; i < 256; i += 8)
            {
                dris.Set4Bytes(bigseed, i, dris.seed1);
                dris.Set4Bytes(bigseed, i + 4, dris.seed2);
            }
            for (i = 0; i < 256; i++)
                S[i] = (byte) i;
            for (i = 0, j = 0; i < 256; i++)
            {
                j = (j + S[i] + bigseed[i] + 7) % 256;
                temp = S[i];
                S[i] = S[j];
                S[j] = temp;
            }
            for (i = 0, j = 0, k = 0; k < length; k++)
            {
                i = (i + 1) % 256;
                j = (j + S[i] + 71) % 256;
                temp = S[i];
                S[i] = S[j];
                S[j] = temp;
                t = (byte) (S[i] + S[j] + 16);
                data[k] ^= S[t];
            }
        }

        public override void CryptDRIS(DRIS dris)
        {
            int i, j, k;
            var bigseed = new byte[256];
            var S = new byte[256];
            byte temp, t;
            var dris_bytes = new byte[Marshal.SizeOf(dris)];

            dris.DrisToByteArray(dris, dris_bytes); // convert DRIS to byte array so we can encrypt it

            for (i = 0; i < 256; i += 8)
            {
                dris.Set4Bytes(bigseed, i, dris.seed1);
                dris.Set4Bytes(bigseed, i + 4, dris.seed2);
            }
            for (i = 0; i < 256; i++)
                S[i] = (byte) i;
            for (i = 0, j = 0; i < 256; i++)
            {
                j = (j + S[i] + bigseed[i] + 11) % 256;
                temp = S[i];
                S[i] = S[j];
                S[j] = temp;
            }
            for (i = 0, j = 0, k = 16; k < Marshal.SizeOf(dris); k++)
            {
                i = (i + 1) % 256;
                j = (j + S[i] + 37) % 256;
                temp = S[i];
                S[i] = S[j];
                S[j] = temp;
                t = (byte) (S[i] + S[j] + 17);
                dris_bytes[k] ^= S[t];
            }
            dris.ByteArrayToDris(dris_bytes, dris); // convert it back again
        }

        protected override void SetDrisAlgorithmValues()
        {
            dris_alg_val_a = 10;
            dris_alg_val_b = 7;
            dris_alg_val_c = 13;
            dris_alg_val_d = 8;
            dris_alg_val_e = 6;
            dris_alg_val_f = 16;
            dris_alg_val_g = 9;
            dris_alg_val_h = 2;
        }
    }
}