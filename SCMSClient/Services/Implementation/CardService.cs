using SCMSClient.Models;
using SCMSClient.Services.Interfaces;

namespace SCMSClient.Services.Implementation
{
    public class CardService : AbstractService<Card>, ICardService
    {
        public CardService(IHTTPService httpService) : base(httpService)
        {
            getAllUrl = string.Empty;
        }
    }
}
