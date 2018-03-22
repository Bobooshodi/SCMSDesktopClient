using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CardEncoderLib
{
    /// <summary>
    /// Mifare Keys Data Structure
    /// </summary>
    [Serializable]
    public class MiFareKey
    {
        #region Instance Variable

        public string[] KeyA { get; set; }
        public string[] KeyB { get; set; }
        public string[] UseKey { get; set; }

        public bool Loaded = false;

        #endregion Instance Variable

        #region Constructor

        public MiFareKey()
        {
            KeyA = new string[40];
            KeyB = new string[40];
            UseKey = new string[40];
        }

        #endregion Constructor

        #region Class Methods

        /// <summary>
        /// This method decrypts and loads mifare keys into memory from file
        /// </summary>
        /// <param name="encryptedFileName">Mifare Key filename</param>
        /// <param name="encryptionKey">Encryption Key</param>
        public void LoadKeys(string encryptedFileName, string encryptionKey)
        {
            try
            {
                string stringVal = FileEncryptor.DecryptToString(encryptedFileName, encryptionKey);
                stringVal = stringVal.Replace("\r\n", "$");
                string[] val = stringVal.Split('$');
                string[] item;

                for (int i = 0; i < val.Length; i++)
                {
                    item = val[i].Split('=');

                    for (int j = 0; j < 40; j++)
                    {
                        if (item[0].Equals("Ka" + j))
                        {
                            KeyA[j] = item[1];
                        }

                        if (item[0].Equals("Kb" + j))
                        {
                            KeyB[j] = item[1];
                        }

                        if (item[0].Equals("UseKey" + j))
                        {
                            UseKey[j] = item[1];
                        }
                    }
                }

                Loaded = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method loads Mifare keys into memory from file
        /// </summary>
        /// <param name="fileName">Mifare Key filename</param>
        public void LoadKeys(string fileName)
        {
            try
            {
                string[] val = File.ReadAllLines(fileName);
                MiFareKey mkey = new MiFareKey();
                string[] item;

                for (int i = 0; i < val.Length; i++)
                {
                    item = val[i].Split('=');

                    for (int j = 0; j < 40; j++)
                    {
                        if (item[0].Equals("Ka" + j))
                        {
                            KeyA[j] = item[1];
                        }

                        if (item[0].Equals("Kb" + j))
                        {
                            KeyB[j] = item[1];
                        }

                        if (item[0].Equals("UseKey" + j))
                        {
                            UseKey[j] = item[1];
                        }
                    }
                }
                Loaded = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method saves a Mifare keys as objects in a file
        /// </summary>
        /// <param name="file"></param>
        public void Save(string file)
        {
            try
            {
                IFormatter serializer = new BinaryFormatter();
                using (FileStream saveFileStream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    serializer.Serialize(saveFileStream, getObject());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private MiFareKey getObject()
        {
            return this;
        }

        #endregion Class Methods
    }
}