using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using System.Collections.Generic;

namespace SCMSClient.Services.Implementation
{
    public class CardService : AbstractService<Card>, ICardService
    {
        public CardService(IHTTPService httpService) : base(httpService)
        {
            getUrl = ApiEndpoints.AllCards;
            getAllUrl = ApiEndpoints.FindCardById;
            updateUrl = ApiEndpoints.UpdateCard;
            createUrl = ApiEndpoints.CreateCard;
            deleteUrl = ApiEndpoints.DeleteCard;
        }
    }
}