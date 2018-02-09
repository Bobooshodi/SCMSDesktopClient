using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;

namespace SCMSClient.Services.Implementation
{
    public class CardholderService : AbstractService<Cardholder>, ICardholderService
    {
        public CardholderService(IHTTPService httpService) : base(_httpService: httpService)
        {
            getUrl = ApiEndpoints.FindCardholderById;
            getAllUrl = ApiEndpoints.AllCardholders;
            updateUrl = ApiEndpoints.UpdateCardholder;
            createUrl = ApiEndpoints.CreateCardholder;
            deleteUrl = ApiEndpoints.DeleteCardholder;
        }
    }
}
