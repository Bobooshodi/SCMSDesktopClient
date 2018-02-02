using Newtonsoft.Json;
using SCMSClient.Utilities;
using System;
using System.IO;

namespace TestLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var objects = RandomDataGenerator.CardRequests(50);

            const string folder = @"D:\Projects\VisualStudio2017\repos\SCMS\SCMSClient\SCMSClient\Data";
            const string file = "db.json";

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
            }
            catch (Exception e)
            {
                Console.WriteLine($"An Error Occured: {e}");
            }
        }
    }
}
