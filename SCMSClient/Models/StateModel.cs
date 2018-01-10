namespace SCMSClient.Models
{
    public class State : BaseModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string ShortForm { get; set; }
    }

    public class City : BaseModel
    {
        public string Name { get; set; }
        public State State { get; set; }
        public int Postcode { get; set; }
    }
}
