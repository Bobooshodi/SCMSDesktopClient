using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;

namespace SCMSClient.Services.Implementation
{
    public class TenantService : AbstractService<Tenant>, ITenantService
    {
        public TenantService(IHTTPService httpService) : base(_httpService: httpService)
        {
            getUrl = ApiEndpoints.FindTenantById;
            getAllUrl = ApiEndpoints.AllTenants;
            updateUrl = ApiEndpoints.UpdateTenant;
            createUrl = ApiEndpoints.CreateTenant;
            deleteUrl = ApiEndpoints.DeleteTenant;
        }
    }
}
