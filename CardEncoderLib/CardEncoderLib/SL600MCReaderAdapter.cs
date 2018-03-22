namespace CardEncoderLib
{
    public class SL600MCReaderAdapter : ReaderAdapter
    {
        private CardReader cardReader;

        public CardReader GetCardReader()
        {
            cardReader = new SL600MCReader();

            return cardReader;
        }
    }
}