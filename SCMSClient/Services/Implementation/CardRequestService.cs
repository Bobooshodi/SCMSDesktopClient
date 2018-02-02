using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;

namespace SCMSClient.Services.Implementation
{
    public class CardRequestService : AbstractService<SOACardRequest>, ICardRequestService
    {
        public CardRequestService(IHTTPService _httpService) : base(_httpService)
        {
            getUrl = ApiEndpoints.AllCardRequests;
            getAllUrl = ApiEndpoints.FindCardRequestById;
            updateUrl = ApiEndpoints.UpdateCardRequest;
            createUrl = ApiEndpoints.CreateCardRequest;
            deleteUrl = ApiEndpoints.DeleteCardRequest;
        }
    }
}
