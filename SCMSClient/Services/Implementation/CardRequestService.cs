using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using System.Collections.Generic;

namespace SCMSClient.Services.Implementation
{
    public class CardRequestService : AbstractService<SOACardRequest>, ICardRequestService
    {
        public CardRequestService(IHTTPService _httpService) : base(_httpService)
        {
            getUrl = ApiEndpoints.FindCardRequestById;
            getAllUrl = ApiEndpoints.AllCardRequests;
            updateUrl = ApiEndpoints.UpdateCardRequest;
            createUrl = ApiEndpoints.CreateCardRequest;
            deleteUrl = ApiEndpoints.DeleteCardRequest;
        }
    }
}