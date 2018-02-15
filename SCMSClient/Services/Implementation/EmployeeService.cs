using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;

namespace SCMSClient.Services.Implementation
{
    public class EmployeeService : AbstractService<Employee>, IEmployeeService
    {
        public EmployeeService(IHTTPService httpService) : base(_httpService: httpService)
        {
            getUrl = ApiEndpoints.FindEmployeeById;
            getAllUrl = ApiEndpoints.AllEmployees;
            updateUrl = ApiEndpoints.UpdateEmployee;
            createUrl = ApiEndpoints.CreateEmployee;
            deleteUrl = ApiEndpoints.DeleteEmployee;
        }
    }
}
