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
    public class Cards : BaseService<Card>
    {
        private Mock<IHTTPService> fakeHttpService = new Mock<IHTTPService>();
        private AbstractService<Card> cardService;

        public Cards()
        {
            Setup();

            cardService = new CardService(fakeHttpService.Object);

            ServiceClass = cardService;
        }

        protected override void Setup()
        {
            fakeHttpService.Setup(s => s.Put(new Card(), ApiEndpoints.UpdateCard)).Returns(new Card());
            fakeHttpService.Setup(s => s.Put<Card>(null, ApiEndpoints.UpdateCard)).Throws(new Exception());

            fakeHttpService.Setup(s => s.Post(new Card(), ApiEndpoints.CreateCard)).Returns(new Card());
            fakeHttpService.Setup(s => s.Post<Card>(null, ApiEndpoints.CreateCard)).Throws(new Exception());

            fakeHttpService.Setup(s => s.Get<Card>($"{ApiEndpoints.FindCardById}/object")).Returns(new Card());
            fakeHttpService.Setup(s => s.Get<Card>($"{ApiEndpoints.FindCardById}/exception")).Throws(new Exception());

            fakeHttpService.Setup(s => s.Delete<Card>($"{ApiEndpoints.DeleteCard}/object")).Returns(new Card());
            fakeHttpService.Setup(s => s.Delete<Card>($"{ApiEndpoints.DeleteCard}/exception")).Throws(new Exception());

            fakeHttpService.Setup(s => s.GetAll<Card>($"{ApiEndpoints.AllCards}/exception")).Throws(new Exception());
            fakeHttpService.Setup(s => s.GetAll<Card>($"{ApiEndpoints.AllCards}/object")).Returns(new List<Card>());
        }
    }
}
