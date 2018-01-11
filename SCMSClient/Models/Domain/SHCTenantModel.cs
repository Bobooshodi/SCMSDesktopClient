using System.Collections.Generic;

namespace SCMSClient.Models
{
    public class SHCTenant : BaseModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BuildingId { get; set; }
        public string Building { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public List<AccessGroup> AccessGroups { get; set; }
    }
}