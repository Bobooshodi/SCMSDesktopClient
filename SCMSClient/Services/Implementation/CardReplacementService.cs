using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;

namespace SCMSClient.Services.Implementation
{
    public class CardReplacementService : AbstractService<SOAReplaceCardRequest>, ICardReplacementService
    {
        public CardReplacementService(IHTTPService _httpService) : base(_httpService)
        {
            getUrl = ApiEndpoints.FindCardReplacementRequestById;
            getAllUrl = ApiEndpoints.AllCardReplacementRequests;
            updateUrl = ApiEndpoints.UpdateCardReplacementRequest;
            createUrl = ApiEndpoints.CreateCardReplacementRequest;
            deleteUrl = ApiEndpoints.DeleteCardReplacementRequest;
        }
    }
}