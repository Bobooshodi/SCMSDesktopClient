using Moq;
using NUnit.Framework;
using SCMSClient.Services.Interfaces;
using SCMSClient.ViewModel;

namespace SCMSClientTests.ViewModels
{
    [TestFixture]
    public class CardRegistration
    {
        [Test]
        public void LoadAll_PageLoad_LoadAllCardTypes()
        {
            var fakeCardService = new Mock<ICardService>();
            var fakeCardTypeService = new Mock<ICardTypeService>();
            var fakeCardVendorService = new Mock<ICardVendorService>();

            var vmToTest = new CardRegistrationVM(fakeCardService.Object, fakeCardTypeService.Object,
                fakeCardVendorService.Object);

            fakeCardTypeService.Verify(s => s.GetAll());
        }

        [Test]
        public void LoadAll_PageLoad_LoadAllCardVendors()
        {
            var fakeCardService = new Mock<ICardService>();
            var fakeCardTypeService = new Mock<ICardTypeService>();
            var fakeCardVendorService = new Mock<ICardVendorService>();

            var vmToTest = new CardRegistrationVM(fakeCardService.Object, fakeCardTypeService.Object,
                fakeCardVendorService.Object);

            fakeCardVendorService.Verify(s => s.GetAll());
        }
    }
}
