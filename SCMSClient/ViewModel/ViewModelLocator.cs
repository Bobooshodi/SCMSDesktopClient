/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:SCMSClient"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SCMSClient.Services.Implementation;
using SCMSClient.Services.Interfaces;

namespace SCMSClient.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                SimpleIoc.Default.Register<IUserService, UserService>();
                SimpleIoc.Default.Register<ICardService, CardService>();
                SimpleIoc.Default.Register<IHTTPService, HTTPRequestService>();
                SimpleIoc.Default.Register<ISettingsService, SettingsService>();
                SimpleIoc.Default.Register<IAuthenticationService, AuthenticationService>();
            }
            else
            {
                // Create run time view services and models
                SimpleIoc.Default.Register<IUserService, UserService>();
                SimpleIoc.Default.Register<ICardService, CardService>();
                SimpleIoc.Default.Register<IHTTPService, HTTPRequestService>();
                SimpleIoc.Default.Register<ISettingsService, SettingsService>();
                SimpleIoc.Default.Register<IAuthenticationService, AuthenticationService>();
            }

            SimpleIoc.Default.Register<RequestsVM>();
            SimpleIoc.Default.Register<DashboardVM>();
            SimpleIoc.Default.Register<MainWindowVM>();
            SimpleIoc.Default.Register<AccessUnitsVM>();
            SimpleIoc.Default.Register<CardVendorsVM>();
            SimpleIoc.Default.Register<CardholdersVM>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<BusinessUnitsVM>();
            SimpleIoc.Default.Register<CardInventoryVM>();
            SimpleIoc.Default.Register<SystemOperatorsVM>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        /// <summary>
        /// The DataContext of the MainWindow.Xaml view
        /// </summary>
        public MainWindowVM MainWindow
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowVM>();
            }
        }

        /// <summary>
        /// The DataContext of the Cardholders view
        /// </summary>
        public CardholdersVM Cardholders
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CardholdersVM>();
            }
        }

        /// <summary>
        /// The DataContext of the Card Inventory view
        /// </summary>
        public CardInventoryVM CardInventory
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CardInventoryVM>();
            }
        }

        /// <summary>
        /// The DataContext of the Requests view
        /// </summary>
        public RequestsVM Requests
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RequestsVM>();
            }
        }

        /// <summary>
        /// The DataContext of the Card Vendors view
        /// </summary>
        public CardVendorsVM CardVendors
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CardVendorsVM>();
            }
        }

        /// <summary>
        /// The DataContext of the Business Units view
        /// </summary>
        public BusinessUnitsVM BusinessUnits
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BusinessUnitsVM>();
            }
        }

        /// <summary>
        /// The DataContext of the System Operators view
        /// </summary>
        public SystemOperatorsVM SystemOperators
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SystemOperatorsVM>();
            }
        }

        /// <summary>
        /// The DataContext of the Access Units view
        /// </summary>
        public AccessUnitsVM AccessUnits
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AccessUnitsVM>();
            }
        }

        /// <summary>
        /// The DataContext of the Dashboard view
        /// </summary>
        public DashboardVM Dashboard
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DashboardVM>();
            }
        }

        /// <summary>
        /// The DataContext of the Login Window
        /// </summary>
        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}