using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CardEncoderLib
{
    internal class SL600MCReader : CardReader
    {
        #region DLLImports

        //Dll Imports for NFC reader
        [DllImport("kernel32.dll")]
        private static extern void Sleep(int dwMilliseconds);

        [DllImport("MasterRD.dll")]
        private static extern int lib_ver(ref uint pVer);

        [DllImport("MasterRD.dll")]
        private static extern int rf_init_usb(int HIDNum);

        [DllImport("MasterRD.dll")]
        private static extern int rf_antenna_sta(short icdev, byte mode);

        [DllImport("MasterRD.dll")]
        private static extern int rf_get_device_name(int HIDNum, IntPtr deviceNameBuffer, int size);

        [DllImport("MasterRD.dll")]
        private static extern int rf_init_type(short icdev, byte type);

        [DllImport("MasterRD.dll")]
        private static extern int rf_request(short icdev, byte mode, ref ushort pTagType);

        [DllImport("MasterRD.dll")]
        private static extern int rf_anticoll(short icdev, byte bcnt, IntPtr pSnr, ref byte pRLength);

        [DllImport("MasterRD.dll")]
        private static extern int rf_select(short icdev, IntPtr pSnr, byte srcLen, ref sbyte Size);

        [DllImport("MasterRD.dll")]
        private static extern int rf_halt(short icdev);

        [DllImport("MasterRD.dll")]
        private static extern int rf_M1_authentication2(short icdev, byte mode, byte secnr, IntPtr key);

        [DllImport("MasterRD.dll")]
        private static extern int rf_M1_initval(short icdev, byte adr, Int32 value);

        [DllImport("MasterRD.dll")]
        private static extern int rf_M1_increment(short icdev, byte adr, Int32 value);

        [DllImport("MasterRD.dll")]
        private static extern int rf_M1_decrement(short icdev, byte adr, Int32 value);

        [DllImport("MasterRD.dll")]
        private static extern int rf_M1_readval(short icdev, byte adr, ref Int32 pValue);

        [DllImport("MasterRD.dll")]
        private static extern int rf_M1_read(short icdev, byte adr, IntPtr pData, ref byte pLen);

        [DllImport("MasterRD.dll")]
        private static extern int rf_M1_write(short icdev, byte adr, IntPtr pData);

        [DllImport("MasterRD.dll")]
        private static extern int rf_light(short icdev, byte color);

        [DllImport("MasterRD.dll")]
        private static extern int rf_beep(short icdev, byte msec);

        [DllImport("MasterRD.dll")]
        private static extern int rf_init_device_number(short icdev);

        [DllImport("MasterRD.dll")]
        private static extern int rf_get_device_number(IntPtr icdev);

        [DllImport("MasterRD.dll")]
        private static extern int rf_get_model(short icdev, IntPtr pVersion, ref byte pLen);

        [DllImport("MasterRD.dll")]
        private static extern int rf_get_snr(short icdev, IntPtr pVersion);

        [DllImport("MasterRD.dll")]
        private static extern int rf_ClosePort(int m_hFileHandle);

        [DllImport("MasterRD.dll")]
        private static extern int rf_GetErrorMessage();

        [DllImport("MasterRD.dll")]
        private static extern int rf_free(IntPtr pData);

        #endregion DLLImports

        #region Instance Variables

        public static string VID = "0741";
        public static string PID = "a112";
        private const int ICDEV = 0x0000;
        private const int HIDNO = 0;
        private bool online = false;
        private bool connected = false;
        private byte type;
        private byte mode;
        private ushort tagType;
        private byte byteCount;
        private IntPtr pSnr;
        private byte len;
        private sbyte size;
        private int readerHandle;

        #endregion Instance Variables

        public SL600MCReader()
        {
            type = (byte)'A';
            mode = 0x52;   //0x52 request all; 0x26 request standard
            tagType = 0;
            byteCount = 0x04;
            len = 255;
            size = 0;
        }

        public bool ResetAntenna()
        {
            int status = -1;

            try
            {
                status = rf_antenna_sta(ICDEV, 0);//Turn off RF Transmittal
                if (status != 0)
                    return false;

                Sleep(20);
                status = rf_init_type(ICDEV, type);//Set Reader Working Mode
                if (status != 0)
                    return false;

                Sleep(20);
                status = rf_antenna_sta(ICDEV, 1);//Turn on RF Transmittal
            }
            catch (Exception)
            {
                throw;
            }
            if (status != 0)
                return false;

            return true;
        }

        public bool SetRequestMode()
        {
            int status = -1;

            status = rf_request(ICDEV, mode, ref tagType);//set request mode

            if (status != 0)
            {
                return false;
            }

            return true;
        }

        public bool IsConnected()
        {
            return connected;
        }

        public bool Connect()
        {
            if (IsConnected())
            {
                Disconnect();
            }

            try
            {
                readerHandle = rf_init_usb(HIDNO);
                if (readerHandle != -1 && readerHandle != 0)
                {
                    connected = true;
                }
                else
                {
                    connected = false;
                }
            }
            catch
            {
                throw;
            }

            return connected;
        }

        public bool Disconnect()
        {
            if (connected)
            {
                //return true if disconnect succeeds
                int val = rf_ClosePort(readerHandle);
                if (val == 0)
                {
                    connected = false;
                    return true;
                }
                else
                {
                    ResetAntenna();
                    rf_ClosePort(readerHandle);
                    readerHandle = -1;
                }
            }
            return false;
        }

        public bool RequestCardSerial(MifareCard card)
        {
            int status = -1;
            string cardSerialNumber = String.Empty;

            if (IsConnected())
            {
                try
                {
                    pSnr = Marshal.AllocHGlobal(1024); //Allocate 1Kb of memory

                    for (int i = 0; i < 2; i++) //try 3 times
                    {
                        if (i > 0 && !SetRequestMode()) //reset antenna for second and third try only
                        {
                            ResetAntenna(); //set reader working type and reset

                            Sleep(50);
                            continue;
                        }

                        if (i < 1 && !SetRequestMode())
                        {
                            continue;
                        }

                        status = rf_anticoll(ICDEV, byteCount, pSnr, ref len);//request serial number and lenght of data
                        if (status != 0)
                        {
                            continue;
                        }

                        status = rf_select(ICDEV, pSnr, len, ref size);//selecting the card
                        if (status != 0)
                        {
                            continue;
                        }

                        byte[] seriaLNumberInBytes = new byte[len];

                        for (int j = 0; j < len; j++)
                        {
                            seriaLNumberInBytes[j] = Marshal.ReadByte(pSnr, j);
                        }

                        for (int q = 0; q < len; q++)
                        {
                            cardSerialNumber += RadioUtilities.byteToHEX(seriaLNumberInBytes[q]);
                        }

                        card.SetCardSerial(cardSerialNumber);
                        return true;
                    }

                    return false;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new Exception("Failed to connect to reader");
            }
        }

        public string RequestReaderSerial()
        {
            int len = 8;
            int returnVal = -1;

            if (IsConnected())
            {
                try
                {
                    pSnr = Marshal.AllocHGlobal(1024); //Allocate 1Kb of memory

                    returnVal = rf_get_snr(ICDEV, pSnr);//request serial number and lenght of data

                    byte[] readerSerialNumberInBytes = new byte[len];

                    for (int j = 0; j < len; j++)
                    {
                        readerSerialNumberInBytes[j] = Marshal.ReadByte(pSnr, j);
                    }

                    Marshal.FreeHGlobal(pSnr);

                    return Encoding.ASCII.GetString(readerSerialNumberInBytes);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new Exception("Failed to connect to reader.");
            }
        }

        public string RequestReaderModel()
        {
            int status = -1;
            string readerModel = String.Empty;

            if (IsConnected())
            {
                try
                {
                    pSnr = Marshal.AllocHGlobal(1024); //Allocate 1Kb of memory

                    status = rf_get_model(ICDEV, pSnr, ref len);//request serial number and lenght of data

                    byte[] readerModelInBytes = new byte[len];

                    for (int j = 0; j < len; j++)
                    {
                        readerModelInBytes[j] = Marshal.ReadByte(pSnr, j);
                    }

                    for (int q = 0; q < len; q++)
                    {
                        readerModel += (Char)(readerModelInBytes[q]);
                    }

                    Marshal.FreeHGlobal(pSnr);

                    return readerModel;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new Exception("Failed to connect to reader.");
            }
        }

        public bool AuthenticateSector(MifareCard card, int sectorNumber)
        {
            int status = -1;
            byte mode = card.GetAuthenticationMode();
            byte sectorNumberInBytes = 0x00;

            if (IsConnected())
            {
                if (RequestCardSerial(card))
                {
                    try
                    {
                        sectorNumberInBytes = Convert.ToByte(sectorNumber);

                        IntPtr keyBuffer = Marshal.AllocHGlobal(1024);

                        byte[] bytesKey = RadioUtilities.ToDigitsBytes(card.GetKey(sectorNumber));

                        //Write key to unmanaged memory to be read by dll function
                        for (int i = 0; i < bytesKey.Length; i++)
                            Marshal.WriteByte(keyBuffer, i, bytesKey[i]);

                        status = rf_M1_authentication2(ICDEV, mode, (byte)(sectorNumberInBytes * MifareCard.NumberOfBlocksInASector), keyBuffer);

                        Marshal.FreeHGlobal(keyBuffer);
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    if (status != 0)
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    throw new Exception("No card inserted.");
                }
            }
            else
            {
                throw new Exception("Card Reader not connected.");
            }
        }

        public bool ReadBlock(MifareCard card, int sector, int block)
        {
            if (card.GetCurrentAuthenticatedSector() == sector)
            {
                return ReadBlockWithoutAuth(card, sector, block);
            }
            else
            {
                if (AuthenticateSector(card, sector))
                {
                    return ReadBlockWithoutAuth(card, sector, block);
                }
                else
                {
                    return false;
                }
            }
        }

        public bool WriteBlock(MifareCard card, string data, int sector, int block)
        {
            if (data.Length == MifareCard.NumberOfCharactersInABlock)
            {
                if (card.GetCurrentAuthenticatedSector() == sector)
                {
                    return WriteBlockWithoutAuth(card, data, sector, block);
                }
                else
                {
                    if (AuthenticateSector(card, sector))
                    {
                        return WriteBlockWithoutAuth(card, data, sector, block);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public bool WriteBlock(MifareCard card, byte[] data, int sector, int block)
        {
            if (data.Length == MifareCard.NumberOfBytesInABlock)
            {
                if (card.GetCurrentAuthenticatedSector() == sector)
                {
                    return WriteBlockWithoutAuth(card, data, sector, block);
                }
                else
                {
                    if (AuthenticateSector(card, sector))
                    {
                        return WriteBlockWithoutAuth(card, data, sector, block);
                    }
                    else
                    {
                        throw new Exception("Authentication failed for the specified key."); ;
                    }
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("Data must be 16 bytes.");
            }
        }

        private bool ReadBlockWithoutAuth(MifareCard card, int sector, int blockNo)
        {
            int status = -1;
            byte[] dataInBytes = new byte[MifareCard.NumberOfBytesInABlock];
            byte sectorNo = Convert.ToByte(sector);
            int j;
            byte cLen = 0;

            try
            {
                IntPtr dataBuffer = Marshal.AllocHGlobal(1024);

                status = rf_M1_read(ICDEV, (byte)((sectorNo * MifareCard.NumberOfBlocksInASector) + blockNo), dataBuffer, ref cLen);

                if (status != 0 || cLen != MifareCard.NumberOfBytesInABlock)
                {
                    Marshal.FreeHGlobal(dataBuffer);
                    throw new Exception("Unable to read from card.");
                }

                for (j = 0; j < dataInBytes.Length; j++)
                {
                    dataInBytes[j] = Marshal.ReadByte(dataBuffer, j);
                }

                Marshal.FreeHGlobal(dataBuffer);
            }
            catch (Exception)
            {
                return false;
                throw;
            }

            card.SetDataInBytes(dataInBytes);
            card.SetDataFromLastBlockRead(RadioUtilities.ToHexString(dataInBytes));
            return true;
        }

        private bool WriteBlockWithoutAuth(MifareCard card, string data, int sector, int block)
        {
            int status = -1;
            byte mode = card.GetAuthenticationMode();
            byte sectorNo = Convert.ToByte(sector);
            byte adr;
            int i;

            if (IsConnected())
            {
                adr = (byte)(Convert.ToByte(block) + sectorNo * MifareCard.NumberOfBlocksInASector);

                byte[] bytesBlock = RadioUtilities.ToDigitsBytes(data);

                try
                {
                    IntPtr dataBuffer = Marshal.AllocHGlobal(1024);

                    for (i = 0; i < bytesBlock.Length; i++)
                    {
                        Marshal.WriteByte(dataBuffer, i, bytesBlock[i]);
                    }

                    status = rf_M1_write(ICDEV, adr, dataBuffer);
                    Marshal.FreeHGlobal(dataBuffer);
                }
                catch (Exception)
                {
                    throw;
                }

                if (status != 0)
                {
                    return false;
                }

                return true;
            }
            else
            {
                throw new Exception("Card Reader not connected.");
            }
        }

        private bool WriteBlockWithoutAuth(MifareCard card, byte[] data, int sector, int block)
        {
            int status = -1;
            byte mode = card.GetAuthenticationMode();
            byte sectorNo = Convert.ToByte(sector);
            byte adr;
            int i;

            if (IsConnected())
            {
                adr = (byte)(Convert.ToByte(block) + sectorNo * MifareCard.NumberOfBlocksInASector); //

                byte[] bytesBlock = data;

                try
                {
                    IntPtr dataBuffer = Marshal.AllocHGlobal(1024);

                    for (i = 0; i < bytesBlock.Length; i++)
                    {
                        Marshal.WriteByte(dataBuffer, i, bytesBlock[i]);
                    }

                    status = rf_M1_write(ICDEV, adr, dataBuffer);

                    Marshal.FreeHGlobal(dataBuffer);
                }
                catch (Exception)
                {
                    throw;
                }

                if (status != 0)
                {
                    return false;
                }
                return true;
            }
            else
            {
                throw new Exception("Card Reader not connected.");
            }
        }

        public bool WriteKeyA(MifareCard card, string data, int sector)
        {
            if (data.Length == MifareCard.NumberOfCharactersInKey)
            {
                if (!ReadBlock(card, sector, MifareCard.KeyBlock))
                {
                    return false;
                }

                string originalData = card.GetDataFromLastBlockRead();
                string newData = data + originalData.Substring(MifareCard.NumberOfCharactersInKey);

                if (card.GetCurrentAuthenticatedSector() == sector)
                {
                    return WriteBlockWithoutAuth(card, newData, sector, MifareCard.KeyBlock); //block #3 is sector trailer
                }
                else
                {
                    if (AuthenticateSector(card, sector))
                    {
                        return WriteBlockWithoutAuth(card, newData, sector, MifareCard.KeyBlock);
                    }
                    else
                    {
                        throw new Exception("Authentication failed for the specified key."); ;
                    }
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("The key must be 12 characters.");
            }
        }

        public bool WriteKeyB(MifareCard card, string data, int sector)
        {
            throw new NotImplementedException();
        }

        public void Beep(byte duration)
        {
            //throw new NotImplementedException();
        }

        public void LightLED(byte color)
        {
            int status = -1;

            if (IsConnected())
            {
                status = rf_light(ICDEV, color);
            }
            else
            {
                if (Connect())
                {
                    status = rf_light(ICDEV, color);
                    Disconnect();
                }
            }
        }

        public bool WriteKeys(MifareCard card, string keyA, string keyB, int sector, string accessbits)
        {
            if (keyA.Length == 12 && keyB.Length == 12)
            {
                if (!ReadBlock(card, sector, 3)) return false;

                string keyString = keyA + accessbits + keyB;

                if (card.GetCurrentAuthenticatedSector() == sector)
                {
                    WriteBlockWithoutAuth(card, keyString, sector, 3);
                }
                else
                {
                    if (AuthenticateSector(card, sector))
                    {
                        if (WriteBlockWithoutAuth(card, keyString, sector, 3))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                        return false;
                }
            }

            return false;
        }
    }
}