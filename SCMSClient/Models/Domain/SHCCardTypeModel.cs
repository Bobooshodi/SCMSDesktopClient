using System.Collections.Generic;

namespace SCMSClient.Models
{
    public class SHCCardType : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPermanent { get; set; }
        public CardType CardType { get; set; }
        public List<BusinessUnit> BusinessUnits { get; set; }
    }

    public enum CardType
    {
        Individual = 0,
        Tenant = 1,
        Employee = 2,
        Strata = 3,
    }
}
