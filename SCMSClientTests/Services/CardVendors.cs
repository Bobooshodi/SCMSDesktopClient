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
    public class CardVendors : BaseService<CardVendor>
    {
        private Mock<IHTTPService> fakeHttpService = new Mock<IHTTPService>();
        private AbstractService<CardVendor> CardVendorService;

        public CardVendors()
        {
            Setup();

            CardVendorService = new CardVendorService(fakeHttpService.Object);

            ServiceClass = CardVendorService;
        }

        protected override void Setup()
        {
            fakeHttpService.Setup(s => s.Put(new CardVendor(), ApiEndpoints.UpdateCardVendor)).Returns(new CardVendor());
            fakeHttpService.Setup(s => s.Put<CardVendor>(null, ApiEndpoints.UpdateCardVendor)).Throws(new Exception());

            fakeHttpService.Setup(s => s.Post(new CardVendor(), ApiEndpoints.CreateCardVendor)).Returns(new CardVendor());
            fakeHttpService.Setup(s => s.Post<CardVendor>(null, ApiEndpoints.CreateCardVendor)).Throws(new Exception());

            fakeHttpService.Setup(s => s.Get<CardVendor>($"{ApiEndpoints.FindCardVendorById}/object")).Returns(new CardVendor());
            fakeHttpService.Setup(s => s.Get<CardVendor>($"{ApiEndpoints.FindCardVendorById}/exception")).Throws(new Exception());

            fakeHttpService.Setup(s => s.Delete<CardVendor>($"{ApiEndpoints.DeleteCardVendor}/object")).Returns(new CardVendor());
            fakeHttpService.Setup(s => s.Delete<CardVendor>($"{ApiEndpoints.DeleteCardVendor}/exception")).Throws(new Exception());

            fakeHttpService.Setup(s => s.GetAll<CardVendor>($"{ApiEndpoints.AllCardVendors}/exception")).Throws(new Exception());
            fakeHttpService.Setup(s => s.GetAll<CardVendor>($"{ApiEndpoints.AllCardVendors}/object")).Returns(new List<CardVendor>());
        }
    }
}