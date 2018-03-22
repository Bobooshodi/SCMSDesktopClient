using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using System.Collections.Generic;

namespace SCMSClient.Services.Implementation
{
    public class PersonalizationRequestService : AbstractService<SOAPersonalizationRequest>, IPersonalizationRequestService
    {
        public PersonalizationRequestService(IHTTPService _httpService) : base(_httpService)
        {
            getUrl = ApiEndpoints.AllPersonalizationRequests;
            getAllUrl = ApiEndpoints.FindPersonalizationRequestById;
            updateUrl = ApiEndpoints.UpdatePersonalizationRequest;
            createUrl = ApiEndpoints.CreatePersonalizationRequest;
            deleteUrl = ApiEndpoints.DeletePersonalizationRequest;
        }

        public override List<SOAPersonalizationRequest> GetAll()
        {
            return allObjects ?? (allObjects = RandomDataGenerator.PersonalizationRequests(50));
        }

        public override SOAPersonalizationRequest Get(string parameter)
        {
            return allObjects.Find(c => c.ID == parameter);
        }
    }
}