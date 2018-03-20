namespace CardEncoderLib
{
    public class CardKey
    {
        public string Key { get; set; }
        public int Block { get; set; }
        public int Sector { get; set; }
        public char KeyType { get; set; }
    }
}