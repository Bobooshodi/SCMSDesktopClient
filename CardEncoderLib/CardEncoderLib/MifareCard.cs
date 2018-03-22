using System;

namespace CardEncoderLib
{
    /// <summary>
    /// Mifare Card data structure
    /// </summary>
    public class MifareCard
    {
        #region Instance Variables

        public const string MifareDefaultKey = "FFFFFFFFFFFF";
        public const int MIfareCardKeySector = 3;
        public const int NumberOfCharactersInKey = 12;
        public const int NumberOfBlocksInASector = 4;
        public const int NumberOfBlocksAssessibleInASector = 3;
        public const int NumberOfBytesInABlock = 16;
        public const int NumberOfCharactersInABlock = 32;
        public const int KeyBlock = 3;
        private string cardSerial = "";
        private string[] keyA;    //Authentication Key A
        private string[] keyB;    //Authentication Key B
        private byte authMode;  //Authentication mode (with Key A/B)
        private int currentAuthenticatedSector = -1;
        private string dataFromLastBlockRead = "";
        private string dataForBlockWrite = "";
        private byte[] bytesdata;
        private bool selected = false;
        private int sectors;

        #endregion Instance Variables

        public MifareCard()
        {
            keyA = new string[(int)MifareCardTypes.Mifare1k];
            keyB = new string[(int)MifareCardTypes.Mifare1k];

            //initialize key values to Mifare default
            for (int i = 0; i < keyA.Length; i++)
            {
                SetKeyA(MifareDefaultKey, i);
                SetKeyB(MifareDefaultKey, i);
            }

            SetAuthenticationMode(0); //set default authentication mode to keyA
            sectors = keyA.Length;
        }

        public MifareCard(MifareCardTypes mifareCardType)
        {
            if (mifareCardType == MifareCardTypes.Mifare1k || mifareCardType == MifareCardTypes.Mifare4k)
            {
                keyA = new string[(int)mifareCardType];
                keyB = new string[(int)mifareCardType];

                for (int i = 0; i < (int)mifareCardType; i++)
                {
                    SetKeyA(MifareDefaultKey, i);
                    SetKeyB(MifareDefaultKey, i);
                }
                SetAuthenticationMode(0);
                sectors = keyA.Length;
            }
            else
            {
                throw new Exception("Invalid number of sectors. A mifare cardWithNewKeys must be 16 or 40 sectors");
            }
        }

        public void SetIsSelected(bool value)
        {
            selected = value;
        }

        public bool isSelected()
        {
            return selected;
        }

        public string GetDataFromLastBlockRead()
        {
            return dataFromLastBlockRead;
        }

        public void SetDataFromLastBlockRead(string data)
        {
            dataFromLastBlockRead = data;
        }

        public byte[] GetBytesData()
        {
            return bytesdata;
        }

        public void SetDataInBytes(byte[] data)
        {
            bytesdata = data;
        }

        public string GetDataForBlockWrite()
        {
            return dataForBlockWrite;
        }

        public void GetDataForBlockWrite(string data)
        {
            dataForBlockWrite = data;
        }

        public void SetCardSerial(string serial)
        {
            cardSerial = serial;
        }

        public string GetCardSerial()
        {
            return cardSerial;
        }

        public string GetKey(int id)
        {
            if (authMode == 0x60) return keyA[id];
            else return keyB[id];
        }

        public string GetKeyA(int id)
        {
            return keyA[id];
        }

        public void SetKeyA(string key, int id)
        {
            if (key.Length == 12)
            {
                keyA[id] = key;
            }
            else
            {
                throw new Exception("Key length must be 12 Hex characters");
            }
        }

        public void SetKeyB(string key, int id)
        {
            if (key.Length == 12)
            {
                keyB[id] = key;
            }
            else
            {
                throw new Exception("Key length must be 12 Hex characters");
            }
        }

        public string GetKeyB(int id)
        {
            return keyB[id];
        }

        public bool SetAuthenticationMode(int mode)
        {
            if (mode == 0)
            {
                authMode = 0x60;
            }
            else if (mode == 1)
            {
                authMode = 0x61;
            }
            else
            {
                return false;
            }
            return true;
        }

        public byte GetAuthenticationMode()
        {
            return authMode;
        }

        public void SetCurrentAuthenticatedSector(int sector)
        {
            if ((sector <= sectors) && (sector >= 0))
                currentAuthenticatedSector = sector;
            else
                currentAuthenticatedSector = -1;
        }

        public int GetCurrentAuthenticatedSector()
        {
            return currentAuthenticatedSector;
        }

        public int Sectors
        {
            get
            {
                return sectors;
            }
        }
    }

    public enum MifareCardTypes
    {
        Mifare4k = 40,
        Mifare1k = 16
    }
}