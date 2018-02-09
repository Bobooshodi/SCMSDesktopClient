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
    public class CardTypes : BaseService<CardType>
    {
        private Mock<IHTTPService> fakeHttpService = new Mock<IHTTPService>();
        private AbstractService<CardType> CardTypeService;

        public CardTypes()
        {
            Setup();

            CardTypeService = new CardTypeService(fakeHttpService.Object);

            ServiceClass = CardTypeService;
        }

        protected override void Setup()
        {
            fakeHttpService.Setup(s => s.Put(new CardType(), ApiEndpoints.UpdateCardType)).Returns(new CardType());
            fakeHttpService.Setup(s => s.Put<CardType>(null, ApiEndpoints.UpdateCardType)).Throws(new Exception());

            fakeHttpService.Setup(s => s.Post(new CardType(), ApiEndpoints.CreateCardType)).Returns(new CardType());
            fakeHttpService.Setup(s => s.Post<CardType>(null, ApiEndpoints.CreateCardType)).Throws(new Exception());

            fakeHttpService.Setup(s => s.Get<CardType>($"{ApiEndpoints.FindCardTypeById}/object")).Returns(new CardType());
            fakeHttpService.Setup(s => s.Get<CardType>($"{ApiEndpoints.FindCardTypeById}/exception")).Throws(new Exception());

            fakeHttpService.Setup(s => s.Delete<CardType>($"{ApiEndpoints.DeleteCardType}/object")).Returns(new CardType());
            fakeHttpService.Setup(s => s.Delete<CardType>($"{ApiEndpoints.DeleteCardType}/exception")).Throws(new Exception());

            fakeHttpService.Setup(s => s.GetAll<CardType>($"{ApiEndpoints.AllCardTypes}/exception")).Throws(new Exception());
            fakeHttpService.Setup(s => s.GetAll<CardType>($"{ApiEndpoints.AllCardTypes}/object")).Returns(new List<CardType>());
        }
    }
}
