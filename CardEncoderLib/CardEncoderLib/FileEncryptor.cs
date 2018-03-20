using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CardEncoderLib
{
   public class FileEncryptor
    {
        /// <summary>
        /// This method decrypts a file encrypted with a private key to its
        /// original form.
        /// </summary>
        /// <param name="fileTodecrypt">The file to decrypt</param>
        /// <param name="decryptedFile">The decrypted file</param>
        /// <param name="key">The key used for decryption</param>
        public void Decrypt(string fileTodecrypt, string decryptedFile, string key)
        {
            string EncryptionKey = key;
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsInput = new FileStream(fileTodecrypt, FileMode.Open))
                {
                    using (CryptoStream cs = new CryptoStream(fsInput, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (FileStream fsOutput = new FileStream(decryptedFile, FileMode.Create))
                        {
                            int data;
                            while ((data = cs.ReadByte()) != -1)
                            {
                                fsOutput.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method decrypts a file encrypted with a private key to its
        /// string form.
        /// </summary>
        /// <param name="fileTodecrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DecryptToString(string fileTodecrypt, string key)
        {
            string EncryptionKey = key;
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsInput = new FileStream(fileTodecrypt, FileMode.Open))
                {
                    using (CryptoStream cs = new CryptoStream(fsInput, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        List<byte> lstbyt = new List<byte>();
                        int data;
                        while ((data = cs.ReadByte()) != -1)
                        {
                            lstbyt.Add((byte)data);
                        }

                        return Encoding.ASCII.GetString(lstbyt.ToArray());
                    }
                }
            }
        }
    }
}
