namespace SCMSClient.Models
{
    public class CardDetail : BaseModel
    {
        public string CardId { get; set; }
        public int Sector { get; set; }
        public string Block0 { get; set; }
        public string Block1 { get; set; }
        public string Block2 { get; set; }
    }
}
