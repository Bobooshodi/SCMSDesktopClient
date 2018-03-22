using System;

namespace SCMSClient.Models
{
    public class SOAPersonalizationRequest : BaseModel
    {
        public string RequestId { get; set; }
        public string ContractNo { get; set; }
        public string CardholderId { get; set; }
        public string Cardholder { get; set; }
        public string IdentificationNo { get; set; }
        public IdentificationType IdentificationType { get; set; }
        public string CardInventoryNo { get; set; }
        public CardType CardType { get; set; }
        public RequestStatus PersonalizationStatus { get; set; }
        public DateTime RequestDate { get; set; }
    }

    public enum RequestStatus
    {
        New = 0,
        Processed = 1
    }
}