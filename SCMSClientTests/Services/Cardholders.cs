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
    public class Cardholders : BaseService<Cardholder>
    {
        private Mock<IHTTPService> fakeHttpService = new Mock<IHTTPService>();
        private AbstractService<Cardholder> cardholderService;

        public Cardholders()
        {
            Setup();

            cardholderService = new CardholderService(fakeHttpService.Object);

            ServiceClass = cardholderService;
        }

        protected override void Setup()
        {
            fakeHttpService.Setup(s => s.Put<Cardholder>(null, ApiEndpoints.UpdateCardholder)).Throws(new Exception());
            fakeHttpService.Setup(s => s.Put(new Cardholder(), ApiEndpoints.UpdateCardholder)).Returns(new Cardholder());

            fakeHttpService.Setup(s => s.Post(new Cardholder(), ApiEndpoints.CreateCardholder)).Returns(new Cardholder());
            fakeHttpService.Setup(s => s.Post<Cardholder>(null, ApiEndpoints.CreateCardholder)).Throws(new Exception());

            fakeHttpService.Setup(s => s.Get<Cardholder>($"{ApiEndpoints.FindCardholderById}/object")).Returns(new Cardholder());
            fakeHttpService.Setup(s => s.Get<Cardholder>($"{ApiEndpoints.FindCardholderById}/exception")).Throws(new Exception());

            fakeHttpService.Setup(s => s.Delete<Cardholder>($"{ApiEndpoints.DeleteCardholder}/object")).Returns(new Cardholder());
            fakeHttpService.Setup(s => s.Delete<Cardholder>($"{ApiEndpoints.DeleteCardholder}/exception")).Throws(new Exception());

            fakeHttpService.Setup(s => s.GetAll<Cardholder>($"{ApiEndpoints.AllCardholders}/exception")).Throws(new Exception());
            fakeHttpService.Setup(s => s.GetAll<Cardholder>($"{ApiEndpoints.AllCardholders}/object")).Returns(new List<Cardholder>());
        }
    }
}