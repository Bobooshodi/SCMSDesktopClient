using NUnit.Framework;
using SCMSClient.ViewModel;
using System;

namespace SCMSClientTests.ViewModels
{
    [TestFixture]
    public class MainWindow
    {
        [Test]
        public void Navigate_DashboardParameter_GoesToDashboardPage()
        {
            //var vmToTest = new MainWindowVM();

            //var expected = new Uri("/Views/Dashboard.xaml", UriKind.RelativeOrAbsolute);

            //vmToTest.NavigationCommand.Execute("dashboard");

            //Assert.AreEqual(expected, vmToTest.ActivePage);
        }

        [Test]
        public void Navigate_RequestParameter_GoesToRequestsPage()
        {
            //var vmToTest = new MainWindowVM();

            //var expected = new Uri("/Views/MainRequestPage.xaml", UriKind.RelativeOrAbsolute);

            //vmToTest.NavigationCommand.Execute("requests");

            //Assert.AreEqual(expected, vmToTest.ActivePage);
        }

        [Test]
        public void Navigate_InventoryParameter_GoesToInventoryPage()
        {
            //var vmToTest = new MainWindowVM();

            //var expected = new Uri("/Views/CardInventory.xaml", UriKind.RelativeOrAbsolute);

            //vmToTest.NavigationCommand.Execute("inventory");

            //Assert.AreEqual(expected, vmToTest.ActivePage);
        }

        [Test]
        public void Navigate_CardholderParameter_GoesToCardholdersPage()
        {
            //var vmToTest = new MainWindowVM();

            //var expected = new Uri("/Views/CardholderList.xaml", UriKind.RelativeOrAbsolute);

            //vmToTest.NavigationCommand.Execute("cardholders");

            //Assert.AreEqual(expected, vmToTest.ActivePage);
        }
    }
}
