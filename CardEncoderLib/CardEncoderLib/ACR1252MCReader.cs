using System;

namespace CardEncoderLib
{
    internal class ACR1252MCReader : CardReader
    {
        public bool Connect()
        {
            throw new NotImplementedException();
        }

        public bool Disconnect()
        {
            throw new NotImplementedException();
        }

        public string RequestReaderSerial()
        {
            throw new NotImplementedException();
        }

        public string RequestReaderModel()
        {
            throw new NotImplementedException();
        }

        public bool RequestCardSerial(MifareCard card)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateSector(MifareCard card, int sector)
        {
            throw new NotImplementedException();
        }

        public bool ReadBlock(MifareCard card, int sector, int block)
        {
            throw new NotImplementedException();
        }

        public bool WriteBlock(MifareCard card, string data, int sector, int block)
        {
            throw new NotImplementedException();
        }

        public bool WriteBlock(MifareCard card, byte[] data, int sector, int block)
        {
            throw new NotImplementedException();
        }

        public bool WriteKeyA(MifareCard card, string data, int sector)
        {
            throw new NotImplementedException();
        }

        public bool WriteKeyB(MifareCard card, string data, int sector)
        {
            throw new NotImplementedException();
        }

        public void Beep(byte duration)
        {
            throw new NotImplementedException();
        }

        public void LightLED(byte color)
        {
            throw new NotImplementedException();
        }

        public bool WriteKeys(MifareCard card, string keyA, string keyB, int sector, string accessbits)
        {
            throw new NotImplementedException();
        }
    }
}