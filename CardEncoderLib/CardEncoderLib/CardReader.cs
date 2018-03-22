namespace CardEncoderLib
{
    public interface CardReader : KeysWriter
    {
        bool Connect();

        bool Disconnect();

        string RequestReaderSerial();

        string RequestReaderModel();

        bool RequestCardSerial(MifareCard card);

        bool AuthenticateSector(MifareCard card, int sector);

        bool ReadBlock(MifareCard card, int sector, int block);

        bool WriteBlock(MifareCard card, string data, int sector, int block);

        bool WriteBlock(MifareCard card, byte[] data, int sector, int block);

        bool WriteKeyA(MifareCard card, string data, int sector);

        bool WriteKeyB(MifareCard card, string data, int sector);

        void Beep(byte duration);

        void LightLED(byte color);
    }

    public interface KeysWriter
    {
        bool WriteKeys(MifareCard card, string keyA, string keyB, int sector, string accessbits);
    }
}