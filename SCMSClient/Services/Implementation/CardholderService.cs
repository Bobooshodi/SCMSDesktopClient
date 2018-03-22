using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using System.Collections.Generic;

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

        public override List<Cardholder> GetAll()
        {
            return allObjects ?? (allObjects = RandomDataGenerator.Cardholders(50));
        }

        public override Cardholder Get(string parameter)
        {
            return allObjects.Find(c => c.ID == parameter);
        }
    }
}