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
    public class CardRequests : BaseService<SOACardRequest>
    {
        private Mock<IHTTPService> fakeHttpService = new Mock<IHTTPService>();
        private AbstractService<SOACardRequest> CardRequestService;

        public CardRequests()
        {
            Setup();

            CardRequestService = new CardRequestService(fakeHttpService.Object);

            ServiceClass = CardRequestService;
        }

        protected override void Setup()
        {
            fakeHttpService.Setup(s => s.Put(new SOACardRequest(), ApiEndpoints.UpdateCardRequest)).Returns(new SOACardRequest());
            fakeHttpService.Setup(s => s.Put<SOACardRequest>(null, ApiEndpoints.UpdateCardRequest)).Throws(new Exception());

            fakeHttpService.Setup(s => s.Post(new SOACardRequest(), ApiEndpoints.CreateCardRequest)).Returns(new SOACardRequest());
            fakeHttpService.Setup(s => s.Post<SOACardRequest>(null, ApiEndpoints.CreateCardRequest)).Throws(new Exception());

            fakeHttpService.Setup(s => s.Get<SOACardRequest>($"{ApiEndpoints.FindCardRequestById}/object")).Returns(new SOACardRequest());
            fakeHttpService.Setup(s => s.Get<SOACardRequest>($"{ApiEndpoints.FindCardRequestById}/exception")).Throws(new Exception());

            fakeHttpService.Setup(s => s.Delete<SOACardRequest>($"{ApiEndpoints.DeleteCardRequest}/object")).Returns(new SOACardRequest());
            fakeHttpService.Setup(s => s.Delete<SOACardRequest>($"{ApiEndpoints.DeleteCardRequest}/exception")).Throws(new Exception());

            fakeHttpService.Setup(s => s.GetAll<SOACardRequest>($"{ApiEndpoints.AllCardRequests}/exception")).Throws(new Exception());
            fakeHttpService.Setup(s => s.GetAll<SOACardRequest>($"{ApiEndpoints.AllCardRequests}/object")).Returns(new List<SOACardRequest>());
        }
    }
}
