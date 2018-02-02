﻿using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;

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
    }
}
