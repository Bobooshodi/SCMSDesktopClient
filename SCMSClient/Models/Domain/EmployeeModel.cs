using System.Collections.Generic;

namespace SCMSClient.Models
{
    public class Employee : Cardholder
    {
        public Employee()
        {
            Company = new Company();
        }

        public string ContractId { get; set; }
        public string Designation { get; set; }
        public string EmployeeId { get; set; }
        public Company Company { get; set; }
    }
}