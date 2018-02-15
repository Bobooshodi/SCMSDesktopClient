using System;

namespace SCMSClient.Models
{
    public class CarPark : BaseModel
    {
        public string RequestId { get; set; }
        public string CardParkId { get; set; }
        public string BayNo { get; set; }
        public string BayType { get; set; }
        public string BayLocation { get; set; }
        public string Building { get; set; }
        public CarParkStatus Status { get; set; }
        public string CardholderId { get; set; }
        public string Cardholder { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsExpanded { get; set; }
    }

    public enum CarParkStatus
    {
        Active = 0,
        Inative = 1,
        Terminated = 2
    }
}
