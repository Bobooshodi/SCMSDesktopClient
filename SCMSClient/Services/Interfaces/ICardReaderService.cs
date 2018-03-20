namespace SCMSClient.Services.Interfaces
{
    public interface ICardReaderService
    {
        string TruncateReadData(int length);

        string ReadCardSerialNumber();

        StatusCode WriteBlock(int sector, int block, string data, string key, byte authentication_mode);

        string ReadBlock(int sector, int block, string key, byte authentication_mode, bool returnHex = true);

        StatusCode EraseBlock(string data, int sector, int block, string key, byte authentication_mode);

        StatusCode WriteKeys(string[] keyA, string[] keyB);

        StatusCode ResetDefaultKeys(string[] keyB);

        StatusCode WriteKeys_Sector(string KeyA, string KeyB, int sector);

        StatusCode ResetDefaultKeys_Sector(string KeyB, int sector);
    }

    public enum StatusCode
    {
        Success = 0,
        ErrCardNotFound = 1,
        ErrIncorrectKey = 2,
        ErrDeviceNotFound = 3,
        ErrVerificationFailed = 4,
        ErrIncorrectCard = 5
    }
}