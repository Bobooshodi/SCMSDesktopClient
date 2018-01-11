using System.Collections.Generic;

namespace SCMSClient.Models
{
    public class Tenant : Cardholder
    {
        public Tenant()
        {
            SHCTenant = new SHCTenant();
        }

        public SHCTenant SHCTenant { get; set; }
        public List<Building> Buildings { get; set; }
    }
}
