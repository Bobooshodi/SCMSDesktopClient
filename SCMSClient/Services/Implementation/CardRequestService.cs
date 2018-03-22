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
            getUrl = ApiEndpoints.AllCardRequests;
            getAllUrl = ApiEndpoints.FindCardRequestById;
            updateUrl = ApiEndpoints.UpdateCardRequest;
            createUrl = ApiEndpoints.CreateCardRequest;
            deleteUrl = ApiEndpoints.DeleteCardRequest;
        }

        public override List<SOACardRequest> GetAll()
        {
            return allObjects ?? (allObjects = RandomDataGenerator.CardRequests(50));
        }

        public override SOACardRequest Get(string parameter)
        {
            return allObjects.Find(c => c.ID == parameter);
        }
    }
}