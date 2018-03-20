using CardEncoderLib;
using SCMSClient.Services.Interfaces;
using System;
using System.Text;

namespace SCMSClient.Services.Implementation
{
    public class SL600ReaderService : ICardReaderService
    {
        public SL600MCReaderAdapter SL600DeviceAdapter;
        private CardReader reader;

        // private static string BLANK_BLOCK = "00000000000000000000000000000000";

        public static string MifareDefaultKey = "FFFFFFFFFFFF";
        private string CardData = "";
        private string CardSerialNumber = "";
        public static byte mode = 0x96;

        public SL600ReaderService()
        {
            SL600DeviceAdapter = new SL600MCReaderAdapter();
            reader = SL600DeviceAdapter.GetCardReader();
        }

        #region Read, Write & Erase

        public bool IsReaderAvaliable()
        {
            try
            {
                //return SL600DeviceAdapter.GetCardReader().Connect();
                return reader.Connect();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string ReadCardSerialNumber()
        {
            var card = new MifareCard(MifareCardTypes.Mifare4k);

            try
            {
                if (reader != null)
                {
                    if (reader.Connect())
                    {
                        if (reader.RequestCardSerial(card))
                        {
                            SetCardSerialNumber(card.GetCardSerial());
                            reader.Disconnect();
                            return GetCardSerialNumber();
                        }

                        throw new Exception("Card not found");
                    }

                    throw new Exception("Device not found");
                }

                throw new Exception("Device not found");
            }
            catch (Exception)
            {
                reader.Disconnect();
                throw;
            }
            finally
            {
                reader.Disconnect();
            }
        }

        public StatusCode WriteBlock(int sector, int block, string data, string key, byte authentication_mode)
        {
            var MifareCard = new MifareCard(MifareCardTypes.Mifare4k);
            bool WriteStatus = true;

            try
            {
                if (reader != null)
                {
                    if (reader.Connect())
                    {
                        if (authentication_mode == AuthenticationMode.KeyA)
                        {
                            MifareCard.SetKeyA(key, sector);
                            MifareCard.SetAuthenticationMode(0);
                        }
                        else
                        {
                            MifareCard.SetKeyB(key, sector);
                            MifareCard.SetAuthenticationMode(1);
                        }

                        reader.RequestCardSerial(MifareCard);
                        string SecuredData = AddSecurityBytes(MifareCard.GetCardSerial(), data);

                        WriteStatus = reader.WriteBlock(MifareCard, AddTrailingBits(ConvertStringToHex(SecuredData)), sector, block);

                        if (!WriteStatus)
                        {
                            reader.Disconnect();
                            return StatusCode.ErrIncorrectKey;
                        }
                        reader.Disconnect();
                    }
                    else
                    {
                        return StatusCode.ErrDeviceNotFound;
                    }
                }
                else
                {
                    return StatusCode.ErrDeviceNotFound;
                }
                return StatusCode.Success;
            }
            catch (Exception)
            {
                reader.Disconnect();
                throw;
            }
        }

        public string ReadBlock(int sector, int block, string key, byte authentication_mode, bool returnHex = true)
        {
            MifareCard card = new MifareCard(MifareCardTypes.Mifare4k);
            bool readStatus = true;

            try
            {
                if (reader != null)
                {
                    if (reader.Connect())
                    {
                        if (authentication_mode == AuthenticationMode.KeyA)
                        {
                            card.SetKeyA(key, sector);
                            card.SetAuthenticationMode(0);
                        }
                        else
                        {
                            card.SetKeyB(key, sector);
                            card.SetAuthenticationMode(1);
                        }

                        if (reader.ReadBlock(card, sector, block))
                        {
                            if (returnHex)
                            {
                                return card.GetDataFromLastBlockRead();
                            }

                            SetData(ConvertHexToString(card.GetDataFromLastBlockRead()));
                        }
                        else
                        {
                            readStatus = false;
                        }
                    }
                    else
                    {
                        throw new Exception("Device not Found");
                    }
                }
                else
                {
                    throw new Exception("Device not Found");
                }

                if (readStatus)
                {
                    return CardData;
                }

                throw new Exception("Incorrect Key");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                reader.Disconnect();
            }
        }

        public StatusCode EraseBlock(string data, int sector, int block, string key, byte authentication_mode)
        {
            var card = new MifareCard(MifareCardTypes.Mifare4k);
            bool WriteStatus = true;

            try
            {
                if (reader != null)
                {
                    if (reader.Connect())
                    {
                        if (authentication_mode == AuthenticationMode.KeyA)
                        {
                            card.SetKeyA(key, sector);
                            card.SetAuthenticationMode(0);
                        }
                        else
                        {
                            card.SetKeyB(key, sector);
                            card.SetAuthenticationMode(1);
                        }

                        WriteStatus = reader.WriteBlock(card, data, sector, block);
                    }
                    else
                        return StatusCode.ErrDeviceNotFound;
                }
                else
                {
                    return StatusCode.ErrDeviceNotFound;
                }

                if (WriteStatus) return StatusCode.Success;

                return StatusCode.ErrIncorrectKey;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                reader.Disconnect();
            }
        }

        #endregion Read, Write & Erase

        #region Write Keys

        public StatusCode WriteKeys(string[] keyA, string[] keyB)
        {
            var card = new MifareCard(MifareCardTypes.Mifare4k);
            bool WriteStatus = true;

            try
            {
                if (reader != null)
                {
                    if (reader.Connect())
                    {
                        for (int i = 1; i < keyA.Length; i++)
                        {
                            if (i != 15 && !reader.WriteKeys(card, keyA[i], keyB[i], i, AccessBits.KeyARead_KeyBWrite)) // Skip Cardax Sector
                            {
                                throw new Exception("Failed to write keys to sector " + i + ". Hint: Card may not be empty.");
                            }
                        }
                    }
                    else
                    {
                        return StatusCode.ErrDeviceNotFound;
                    }
                }
                else
                {
                    return StatusCode.ErrDeviceNotFound;
                }

                if (WriteStatus)
                {
                    return StatusCode.Success;
                }
                else
                {
                    return StatusCode.ErrIncorrectKey;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                reader.Disconnect();
            }
        }

        public StatusCode ResetDefaultKeys(string[] keyB)
        {
            var card = new MifareCard(MifareCardTypes.Mifare4k);

            try
            {
                if (reader != null)
                {
                    if (reader.Connect())
                    {
                        for (int i = 1; i < keyB.Length; i++)
                        {
                            if (i != 15) // Skip Cardax Sector
                            {
                                card.SetKeyB(keyB[i], i);
                                card.SetAuthenticationMode(1);
                                if (!reader.WriteKeys(card, MifareDefaultKey, MifareDefaultKey, i, AccessBits.Default))
                                {
                                    throw new Exception("Failed to reset keys to sector " + i + ". Hint: Card may not be SHC card.");
                                }
                            }
                        }
                    }
                    else
                    {
                        return StatusCode.ErrDeviceNotFound;
                    }
                }
                else
                {
                    return StatusCode.ErrDeviceNotFound;
                }

                return StatusCode.Success;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                reader.Disconnect();
            }
        }

        public StatusCode WriteKeys_Sector(string KeyA, string KeyB, int sector)
        {
            var card = new MifareCard(MifareCardTypes.Mifare4k);
            bool WriteStatus = true;

            try
            {
                if (reader != null)
                {
                    if (reader.Connect())
                    {
                        WriteStatus = reader.WriteKeys(card, KeyA, KeyB, sector, AccessBits.KeyARead_KeyBWrite);
                    }
                    else
                    {
                        return StatusCode.ErrDeviceNotFound;
                    }
                }
                else
                {
                    return StatusCode.ErrDeviceNotFound;
                }

                if (WriteStatus)
                {
                    return StatusCode.Success;
                }

                return StatusCode.ErrIncorrectKey;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                reader.Disconnect();
            }
        }

        public StatusCode ResetDefaultKeys_Sector(string KeyB, int sector)
        {
            var card = new MifareCard(MifareCardTypes.Mifare4k);
            bool WriteStatus = true;
            try
            {
                if (reader != null)
                {
                    if (reader.Connect())
                    {
                        card.SetKeyB(KeyB, sector);
                        card.SetAuthenticationMode(1);
                        WriteStatus = reader.WriteKeys(card, MifareDefaultKey, MifareDefaultKey, sector, AccessBits.Default);
                    }
                    else
                    {
                        return StatusCode.ErrDeviceNotFound;
                    }
                }
                else
                {
                    return StatusCode.ErrDeviceNotFound;
                }

                if (WriteStatus)
                {
                    return StatusCode.Success;
                }

                return StatusCode.ErrIncorrectKey;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                reader.Disconnect();
            }
        }

        #endregion Write Keys

        #region Utilities

        public void SetCardSerialNumber(string MifareID)
        {
            CardSerialNumber = MifareID;
        }

        public string GetCardSerialNumber()
        {
            return CardSerialNumber;
        }

        public void SetData(string Data)
        {
            if (string.IsNullOrEmpty(Data))
            {
                return;
            }

            if (Data.Length > 15)
            {
                CardData = Data.Substring(4, 11);
            }
            else
            {
                CardData = Data;
            }
        }

        public void Beep(int duration)
        {
            if (reader.Connect())
            {
                reader.Beep(Convert.ToByte(duration));
                reader.Disconnect();
            }
        }

        public void Flash(bool on)
        {
            if (reader.Connect())
            {
                if (on)
                {
                    reader.LightLED(Convert.ToByte(1));
                    reader.Disconnect();
                }
                else
                {
                    reader.LightLED(Convert.ToByte(0));
                    reader.Disconnect();
                }
            }
        }

        public string GetData()
        {
            return CardData.TrimEnd('\0');
        }

        public string AddTrailingBits(string hex)
        {
            for (int i = hex.Length; i < 32; i++)
            {
                hex = hex + "0";
            }
            return hex;
        }

        public string ConvertStringToHex(string ascii)
        {
            try
            {
                if (ascii == null) ascii = "";
                string hex = "";
                int temp;
                foreach (char c in ascii)
                {
                    temp = c;
                    hex += string.Format("{0:x2}", System.Convert.ToInt32(temp.ToString()));
                }
                return hex;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string ConvertHexToString(string hex)
        {
            try
            {
                string asciiString = "";
                while (hex.Length >= 2)
                {
                    asciiString += System.Convert.ToChar(System.Convert.ToInt32(hex.Substring(0, 2), 16)).ToString();
                    hex = hex.Substring(2, hex.Length - 2);
                }
                return asciiString.TrimEnd(new char[] { '\0' });
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string AddSecurityBytes(string MifareID, string data)
        {
            string SecurityString = EncryptOrDecrypt(MifareID.Substring(MifareID.Length - 4, 4), "schc");

            if (data.Length < 12)
            {
                return SecurityString + data;
            }
            else
            {
                return SecurityString + data.Substring(0, 11);
            }
        }

        public string EncryptOrDecrypt(string text, string key)
        {
            var result = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                char character = text[i]; // get character at position 'i'

                uint charCode = (uint)character; // convert the character to unsigned integer

                int keyPosition = i % key.Length; // get key position

                char keyChar = key[keyPosition]; // get character key at keyPosition

                uint keyCode = (uint)keyChar; // convert the character to unsigned integer

                uint combinedCode = charCode ^ keyCode; // implement XOr for the two unsigned integers

                char combinedChar = (char)combinedCode; // get character of the combinedcode

                result.Append(combinedChar); // append it to result
            }

            return result.ToString(); // return the character
        }

        public bool VerifyCard(MifareCard Card)
        {
            string data = ConvertHexToString(Card.GetDataFromLastBlockRead());
            string MifareID = Card.GetCardSerial();

            if (data.Length > 0)
            {
                string SecurityString = EncryptOrDecrypt(data.Substring(0, 4), "schc");

                if (SecurityString.Equals(MifareID.Substring(MifareID.Length - 4, 4)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else return true;
        }

        public string GetSecuredHex(MifareCard Card, string data)
        {
            return AddTrailingBits(ConvertStringToHex(AddSecurityBytes(Card.GetCardSerial(), data)));
        }

        public string GetActualData(string data)
        {
            if (data.Length > 0)
            {
                return data.Substring(4, data.Length - 4);
            }
            else
            {
                return "";
            }
        }

        public string TruncateReadData(int length)
        {
            throw new System.NotImplementedException();
        }

        #endregion Utilities
    }

    public static class AuthenticationMode
    {
        public const byte KeyA = 0x60;
        public const byte KeyB = 0x61;
    }

    public static class AccessBits
    {
        public const string Default = "FF078069";
        public const string KeyARead_KeyBWrite = "78778800";
    }
}