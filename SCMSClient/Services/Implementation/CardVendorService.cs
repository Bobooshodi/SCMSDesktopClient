using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;

namespace SCMSClient.Services.Implementation
{
    public class CardVendorService : AbstractService<CardVendor>, ICardVendorService
    {
        public CardVendorService(IHTTPService httpService) : base(httpService)
        {
            getUrl = ApiEndpoints.AllCardVendors;
            getAllUrl = ApiEndpoints.FindCardVendorById;
            updateUrl = ApiEndpoints.UpdateCardVendor;
            createUrl = ApiEndpoints.CreateCardVendor;
            deleteUrl = ApiEndpoints.DeleteCardVendor;
        }
    }
}