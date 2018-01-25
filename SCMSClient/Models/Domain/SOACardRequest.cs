using System;

namespace SCMSClient.Models
{
    public class SOACardRequest : BaseModel
    {
        public string RequestId { get; set; }
        public int Quantity { get; set; }
        public int QuantityDistributed { get; set; }
        public int QuantityRemaining => Quantity - QuantityDistributed;
        public CardType CardType { get; set; }
        public string BusinessUnit { get; set; }
        public string BusinessUnitId { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedById { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
