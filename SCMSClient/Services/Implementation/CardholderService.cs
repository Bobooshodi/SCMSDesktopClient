using SCMSClient.Models;
using SCMSClient.Services.Interfaces;

namespace SCMSClient.Services.Implementation
{
    public class CardholderService : AbstractService<Cardholder>, ICardholderService
    {
        public CardholderService(IHTTPService httpService) : base(_httpService: httpService)
        {

        }
    }
}
