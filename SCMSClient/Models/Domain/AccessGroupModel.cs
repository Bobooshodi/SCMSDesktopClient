using System.Collections.Generic;

namespace SCMSClient.Models
{
    public class AccessGroup : BaseModel
    {
        public string DisplayName { get; set; }
        public string NameInAccessControl { get; set; }
        public string KeyInAccessControl { get; set; }
        public string AccessControlDetail { get; set; }
        public string AccessControlDetail2 { get; set; }
        public string AccessControlDetail3 { get; set; }
        public string Description { get; set; }
        public string AccessControl { get; set; }
        public string AccessControlId { get; set; }
        public List<Building> Buildings { get; set; }
    }
}