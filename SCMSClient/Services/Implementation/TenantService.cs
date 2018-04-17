using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace SCMSClient.Services.Implementation
{
    public class TenantService : AbstractService<Tenant>, ITenantService
    {
        private readonly ICardholderService cardholderService;

        public TenantService(IHTTPService httpService, ICardholderService _cardholderService) : base(_httpService: httpService)
        {
            cardholderService = _cardholderService;

            getUrl = ApiEndpoints.FindTenantById;
            getAllUrl = ApiEndpoints.AllTenants;
            updateUrl = ApiEndpoints.UpdateTenant;
            createUrl = ApiEndpoints.CreateTenant;
            deleteUrl = ApiEndpoints.DeleteTenant;
        }
    }
}