using Moq;
using NUnit.Framework;
using SCMSClient.Services.Interfaces;
using SCMSClient.ViewModel;

namespace SCMSClientTests.ViewModels
{
    [TestFixture]
    public class CardRequests
    {
        private static Mock<ICardRequestService> fakeCardRequestService = new Mock<ICardRequestService>();
        //private CardRequestsVM vmToTest;

        public CardRequests()
        {
            //vmToTest = new CardRequestsVM(fakeCardRequestService.Object);
        }

        [Test]
        public void LoadAll_OnPageLoad_LoadAllCardRequests()
        {
            fakeCardRequestService.Verify(v => v.GetAll());
        }
    }
}