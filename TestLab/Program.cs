using Newtonsoft.Json;
using SCMSClient.Models;
using SCMSClient.Services.Implementation;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using System;
using System.IO;
using System.Text;

namespace TestLab
{
    public class Program
    {
        private static void Main(string[] args)
        {
        }

        private static void CardReaderTest()
        {
            ICardReaderService readerService = new SL600ReaderService();

            var serialNo = readerService.ReadCardSerialNumber();

            var dataInSector = readerService.ReadBlock(7, 0, "425244423031", 96, false);
        }

        private static void GenerateRandomData()
        {
            var objects = RandomDataGenerator.ReplaceCardRequests(5);

            const string folder = @"D:\Projects\VisualStudio2017\repos\SCMS\SCMSClient\SCMSClient\Data";
            const string file = "database.json";

            var content = JsonConvert.SerializeObject(objects);

            try
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                if (!File.Exists(file))
                {
                    File.WriteAllText(file, content);
                }
                else
                {
                    using (var writer = new StreamWriter(Path.Combine(folder, file), true))
                    {
                        writer.WriteLine(content);
                    }
                }

                Console.WriteLine("Done!!!");

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An Error Occured: {e}");

                Console.ReadKey();
            }
        }

        private static void DinkeyDongleTest()
        {
            var service = new DinkeyDongleService();

            var keysA = RandomDataGenerator.CardKeys(40, 0, 'A');
            var keysB = RandomDataGenerator.CardKeys(40, 0, 'B');

            keysA.AddRange(keysB);

            var dongleData = new DongleData
            {
                CardKeys = keysA,
            };

            var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dongleData));

            //WriteBoundData(data, service);

            var stringData = ReadBoundData(service);

            var dData = JsonConvert.DeserializeObject<DongleData>((string)stringData);
        }

        public static bool WriteBoundData(byte[] data, DinkeyDongleService service)
        {
            byte[] DataToWrite = data;
            bool status = false;
            const double onekb = 1024;
            double totalDataLength = DataToWrite.Length;
            if (DataToWrite.Length > onekb)
            {
                var dataLength = onekb;
                double count = Math.Ceiling(DataToWrite.Length / onekb);

                bool dataWritten = false;
                int counter = 1;
                int offset = 0;
                while (counter <= count)
                {
                    byte[] dataBlockToWrite = new byte[Convert.ToInt64(dataLength)];
                    Array.Copy(DataToWrite, offset, dataBlockToWrite, 0, Convert.ToInt64(dataLength));

                    dataWritten = service.WriteData(dataBlockToWrite, offset);

                    if (dataWritten)
                    {
                        status = true;
                    }

                    //dataLength = (totalDataLength % (onekb * counter) == 0) ? onekb : totalDataLength % (onekb * counter);

                    if (totalDataLength - (onekb * counter) < onekb)
                        dataLength = totalDataLength - (onekb * counter);

                    offset = (int)(onekb * counter);
                    counter++;
                }

                if (dataWritten)
                {
                    status = true;
                }
            }
            else
            {
                if (service.WriteData(DataToWrite, 1))
                {
                    status = true;
                }
            }
            return status;
        }

        public static object ReadBoundData(DinkeyDongleService service)
        {
            double onekb = 1024;
            byte[] dataToRead = new byte[4987];
            double totalDataLength = dataToRead.Length;

            var dataLength = onekb;
            double count = Math.Ceiling(dataToRead.Length / onekb);
            int counter = 1;
            int offset = 0;
            while (counter <= count)
            {
                byte[] dataBlockRead = service.ReadData((int)dataLength, offset);
                Array.Copy(dataBlockRead, 0, dataToRead, offset, (int)dataLength);

                //dataLength = (totalDataLength % (onekb * counter) == 0) ? onekb : totalDataLength % (onekb * counter);

                if (totalDataLength - (onekb * counter) < onekb)
                    dataLength = totalDataLength - (onekb * counter);

                offset = (int)(onekb * counter);
                counter++;
            }
            return Encoding.ASCII.GetString(dataToRead);
        }
    }
}