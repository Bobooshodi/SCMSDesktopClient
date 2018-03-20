using System;

namespace SCMSClient.Models
{
    public class Card : BaseModel
    {
        public string BatchNo { get; set; }
        public string CardInventoryNo { get; set; }
        public string MicroEngineNo { get; set; }
        public string CardaxNo { get; set; }
        public string CarParkNo { get; set; }
        public string RequestId { set; get; }
        public string MifareId { set; get; }
        public string CardTypeId { set; get; }
        public string CardType { set; get; }
        public CardVendor CardVendor { get; set; }
        public CardStatus? Status { set; get; }
        public DateTime? PersonalizedDate { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CompanyId { get; set; }
        public string Company { get; set; }
        public string DistributionId { get; set; }
        public string RegisteredBy { get; set; }
        public string RegisteredById { get; set; }
        public string PersonalizedBy { get; set; }
        public string PersonalizedById { get; set; }
        public bool IsSelected { get; set; }
    }

    public enum CardStatus
    {
        New,
        Replacement,
        Deactivated,
        Active,
        Detached,
        Renewal
    }
}
