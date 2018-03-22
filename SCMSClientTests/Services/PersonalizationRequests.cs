using Moq;
using NUnit.Framework;
using SCMSClient.Models;
using SCMSClient.Services.Implementation;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using System;
using System.Collections.Generic;

namespace SCMSClientTests.Services
{
    [TestFixture]
    public class PersonalizationRequests : BaseService<SOAPersonalizationRequest>
    {
        private Mock<IHTTPService> fakeHttpService = new Mock<IHTTPService>();
        private AbstractService<SOAPersonalizationRequest> PersonalizationRequestService;

        public PersonalizationRequests()
        {
            Setup();

            PersonalizationRequestService = new PersonalizationRequestService(fakeHttpService.Object);

            ServiceClass = PersonalizationRequestService;
        }

        protected override void Setup()
        {
            fakeHttpService.Setup(s => s.Put(new SOAPersonalizationRequest(), ApiEndpoints.UpdatePersonalizationRequest)).Returns(new SOAPersonalizationRequest());
            fakeHttpService.Setup(s => s.Put<SOAPersonalizationRequest>(null, ApiEndpoints.UpdatePersonalizationRequest)).Throws(new Exception());

            fakeHttpService.Setup(s => s.Post(new SOAPersonalizationRequest(), ApiEndpoints.CreatePersonalizationRequest)).Returns(new SOAPersonalizationRequest());
            fakeHttpService.Setup(s => s.Post<SOAPersonalizationRequest>(null, ApiEndpoints.CreatePersonalizationRequest)).Throws(new Exception());

            fakeHttpService.Setup(s => s.Get<SOAPersonalizationRequest>($"{ApiEndpoints.FindPersonalizationRequestById}/object")).Returns(new SOAPersonalizationRequest());
            fakeHttpService.Setup(s => s.Get<SOAPersonalizationRequest>($"{ApiEndpoints.FindPersonalizationRequestById}/exception")).Throws(new Exception());

            fakeHttpService.Setup(s => s.Delete<SOAPersonalizationRequest>($"{ApiEndpoints.DeletePersonalizationRequest}/object")).Returns(new SOAPersonalizationRequest());
            fakeHttpService.Setup(s => s.Delete<SOAPersonalizationRequest>($"{ApiEndpoints.DeletePersonalizationRequest}/exception")).Throws(new Exception());

            fakeHttpService.Setup(s => s.GetAll<SOAPersonalizationRequest>($"{ApiEndpoints.AllPersonalizationRequests}/exception")).Throws(new Exception());
            fakeHttpService.Setup(s => s.GetAll<SOAPersonalizationRequest>($"{ApiEndpoints.AllPersonalizationRequests}/object")).Returns(new List<SOAPersonalizationRequest>());
        }
    }
}