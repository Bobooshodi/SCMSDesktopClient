using Moq;
using NUnit.Framework;
using SCMSClient.Services.Interfaces;
using SCMSClient.ViewModel;

namespace SCMSClientTests.ViewModels
{
    [TestFixture]
    public class CardInventory
    {
        [Test]
        public void LoadAll_OnPageLoad_LoadsAllCards()
        {
            var fakeCardService = new Mock<ICardService>();

            var vmToTest = new CardInventoryVM(fakeCardService.Object);

            fakeCardService.Verify(s => s.GetAll(), Times.Once);
        }
    }
}
