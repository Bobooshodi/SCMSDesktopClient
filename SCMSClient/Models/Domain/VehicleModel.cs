namespace SCMSClient.Models
{
    public class Vehicle : BaseModel
    {
        public string NumberPlate { get; set; }
        public string VehicleModel { get; set; }
        public string Brand { get; set; }
        public string CardholderId { get; set; }
    }
}