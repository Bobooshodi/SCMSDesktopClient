using System;

namespace SCMSClient.Models
{
    public class SOAReplaceCardRequest : BaseModel
    {
        public string ReplacementId { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string CardId { get; set; }
        public string ReplacedCardId { get; set; }
        public string CardholderId { get; set; }
        public string Cardholder { get; set; }
        public string ReplacementReason { get; set; }
        public string ReplacedBy { get; set; }
        public string ReplacedById { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime ReplacedDate { get; set; }
    }
}
