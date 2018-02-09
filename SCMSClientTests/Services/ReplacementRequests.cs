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
    public class ReplacementRequests : BaseService<SOAReplaceCardRequest>
    {
        private Mock<IHTTPService> fakeHttpService = new Mock<IHTTPService>();
        private AbstractService<SOAReplaceCardRequest> ReplacementRequestService;

        public ReplacementRequests()
        {
            Setup();

            ReplacementRequestService = new CardReplacementService(fakeHttpService.Object);

            ServiceClass = ReplacementRequestService;
        }

        protected override void Setup()
        {
            fakeHttpService.Setup(s => s.Put(new SOAReplaceCardRequest(), ApiEndpoints.UpdateCardReplacementRequest)).Returns(new SOAReplaceCardRequest());
            fakeHttpService.Setup(s => s.Put<SOAReplaceCardRequest>(null, ApiEndpoints.UpdateCardReplacementRequest)).Throws(new Exception());

            fakeHttpService.Setup(s => s.Post(new SOAReplaceCardRequest(), ApiEndpoints.CreateCardReplacementRequest)).Returns(new SOAReplaceCardRequest());
            fakeHttpService.Setup(s => s.Post<SOAReplaceCardRequest>(null, ApiEndpoints.CreateCardReplacementRequest)).Throws(new Exception());

            fakeHttpService.Setup(s => s.Get<SOAReplaceCardRequest>($"{ApiEndpoints.FindCardReplacementRequestById}/object")).Returns(new SOAReplaceCardRequest());
            fakeHttpService.Setup(s => s.Get<SOAReplaceCardRequest>($"{ApiEndpoints.FindCardReplacementRequestById}/exception")).Throws(new Exception());

            fakeHttpService.Setup(s => s.Delete<SOAReplaceCardRequest>($"{ApiEndpoints.DeleteCardReplacementRequest}/object")).Returns(new SOAReplaceCardRequest());
            fakeHttpService.Setup(s => s.Delete<SOAReplaceCardRequest>($"{ApiEndpoints.DeleteCardReplacementRequest}/exception")).Throws(new Exception());

            fakeHttpService.Setup(s => s.GetAll<SOAReplaceCardRequest>($"{ApiEndpoints.AllCardReplacementRequests}/exception")).Throws(new Exception());
            fakeHttpService.Setup(s => s.GetAll<SOAReplaceCardRequest>($"{ApiEndpoints.AllCardReplacementRequests}/object")).Returns(new List<SOAReplaceCardRequest>());
        }
    }
}
