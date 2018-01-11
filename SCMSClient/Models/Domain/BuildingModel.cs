using System.Collections.Generic;

namespace SCMSClient.Models
{
    public class Building : BaseModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<AccessGroup> AccessGroups { get; set; }
    }
}