using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;

namespace SCMSClient.Services.Implementation
{
    public class CardTypeService : AbstractService<CardType>, ICardTypeService
    {
        public CardTypeService(IHTTPService httpService) : base(httpService)
        {
            getUrl = ApiEndpoints.FindCardTypeById;
            getAllUrl = ApiEndpoints.AllCardTypes;
            updateUrl = ApiEndpoints.UpdateCardType;
            createUrl = ApiEndpoints.CreateCardType;
            deleteUrl = ApiEndpoints.DeleteCardType;
        }
    }
}