using System.Collections.Generic;

namespace SCMSClient.Models
{
    public class CardType : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPermanent { get; set; }
        public SHCCardType SHCCardType { get; set; }
        public List<BusinessUnit> BusinessUnits { get; set; }
    }

    public enum SHCCardType
    {
        Individual = 0,
        Tenant = 1,
        Employee = 2,
        Strata = 3,
    }
}