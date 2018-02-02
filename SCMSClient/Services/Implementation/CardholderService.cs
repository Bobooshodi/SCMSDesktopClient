using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;

namespace SCMSClient.Services.Implementation
{
    public class CardholderService : AbstractService<Cardholder>, ICardholderService
    {
        public CardholderService(IHTTPService httpService) : base(_httpService: httpService)
        {
            getUrl = ApiEndpoints.AllCardholders;
            getAllUrl = ApiEndpoints.FindCardholderById;
            updateUrl = ApiEndpoints.UpdateCardholder;
            createUrl = ApiEndpoints.CreateCardholder;
            deleteUrl = ApiEndpoints.DeleteCardholder;
        }
    }
}
