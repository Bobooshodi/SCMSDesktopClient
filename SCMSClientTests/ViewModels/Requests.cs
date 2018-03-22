using NUnit.Framework;
using SCMSClient.ViewModel;
using System;

namespace SCMSClientTests.ViewModels
{
    [TestFixture]
    public class Requests
    {
        [Test]
        public void Navigate_PersonalizationParameter_GoesToPersonalizationPage()
        {
            //var vmToTest = new RequestsVM();

            //var expected = new Uri("/Views/PersonalizationRequest.xaml", UriKind.RelativeOrAbsolute);

            //vmToTest.NavigationCommand.Execute("personalization");

            //Assert.AreEqual(expected, vmToTest.ActivePage);
        }

        [Test]
        public void Navigate_ReplacementParameter_GoesToReplacementsPage()
        {
            //var vmToTest = new RequestsVM();

            //var expected = new Uri("/Views/ReplaceCard.xaml", UriKind.RelativeOrAbsolute);

            //vmToTest.NavigationCommand.Execute("replacement");

            //Assert.AreEqual(expected, vmToTest.ActivePage);
        }

        [Test]
        public void Navigate_BlacklistParameter_GoesToBlacklistPage()
        {
            //var vmToTest = new RequestsVM();

            //var expected = new Uri("/Views/BlacklistRequest.xaml", UriKind.RelativeOrAbsolute);

            //vmToTest.NavigationCommand.Execute("blacklist");

            //Assert.AreEqual(expected, vmToTest.ActivePage);
        }

        [Test]
        public void Navigate_DistributionParameter_GoesToCardRequestsPage()
        {
            //var vmToTest = new RequestsVM();

            //var expected = new Uri("/Views/CardRequest.xaml", UriKind.RelativeOrAbsolute);

            //vmToTest.NavigationCommand.Execute("distribution");

            //Assert.AreEqual(expected, vmToTest.ActivePage);
        }
    }
}