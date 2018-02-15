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
            }
            else
            {
                // Create run time view services and models
                SimpleIoc.Default.Register<IUserService, UserService>();
                SimpleIoc.Default.Register<ICardService, CardService>();
                SimpleIoc.Default.Register<ITenantService, TenantService>();
                SimpleIoc.Default.Register<IEmployeeService, EmployeeService>();
                SimpleIoc.Default.Register<ISettingsService, SettingsService>();
                SimpleIoc.Default.Register<ICardTypeService, CardTypeService>();
                SimpleIoc.Default.Register<IHTTPService, HTTPRequestServiceDev>(); //TODO: Change Back to the Original with Refreshing ablilities
                SimpleIoc.Default.Register<ICardVendorService, CardVendorService>();
                SimpleIoc.Default.Register<ICardholderService, CardholderService>();
                SimpleIoc.Default.Register<ICardRequestService, CardRequestService>();
                SimpleIoc.Default.Register<IAuthenticationService, AuthenticationService>();
                SimpleIoc.Default.Register<ICardReplacementService, CardReplacementService>();
                SimpleIoc.Default.Register<IPersonalizationRequestService, PersonalizationRequestService>();
            }

            SimpleIoc.Default.Register<RequestsVM>();
            SimpleIoc.Default.Register<DashboardVM>();
            SimpleIoc.Default.Register<MainWindowVM>();
            SimpleIoc.Default.Register<CardholdersVM>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CardRequestsVM>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<CardInventoryVM>();
            SimpleIoc.Default.Register<CardReplacementVM>();
            SimpleIoc.Default.Register<SystemOperatorsVM>();
            SimpleIoc.Default.Register<CardRegistrationVM>();
            SimpleIoc.Default.Register<CardDistributionVM>();
            SimpleIoc.Default.Register<BlacklistRequestVM>();
            SimpleIoc.Default.Register<CardholderDetailsVM>();
            SimpleIoc.Default.Register<ReplaceCardRequestVM>();
            SimpleIoc.Default.Register<CardPersonalizationVM>();
            SimpleIoc.Default.Register<PersonalizationRequestVM>();
        }

        public MainViewModel Main
        {
            get => ServiceLocator.Current.GetInstance<MainViewModel>();
        }

        /// <summary>
        /// The DataContext of the MainWindow.Xaml view
        /// </summary>
        public MainWindowVM MainWindow
        {
            get => ServiceLocator.Current.GetInstance<MainWindowVM>();
        }

        /// <summary>
        /// The DataContext of the Cardholders view
        /// </summary>
        public CardholdersVM Cardholders
        {
            get => ServiceLocator.Current.GetInstance<CardholdersVM>();
        }

        /// <summary>
        /// The DataContext of the Cardholder Details view
        /// </summary>
        public CardholderDetailsVM CardholderDetails
        {
            get => ServiceLocator.Current.GetInstance<CardholderDetailsVM>();
        }

        /// <summary>
        /// The DataContext of the Card Inventory view
        /// </summary>
        public CardInventoryVM CardInventory
        {
            get => ServiceLocator.Current.GetInstance<CardInventoryVM>();
        }

        /// <summary>
        /// The DataContext of the Requests view
        /// </summary>
        public RequestsVM Requests
        {
            get => ServiceLocator.Current.GetInstance<RequestsVM>();
        }

        /// <summary>
        /// The DataContext of the System Operators view
        /// </summary>
        public SystemOperatorsVM SystemOperators
        {
            get => ServiceLocator.Current.GetInstance<SystemOperatorsVM>();
        }

        /// <summary>
        /// The DataContext of the Dashboard view
        /// </summary>
        public DashboardVM Dashboard
        {
            get => ServiceLocator.Current.GetInstance<DashboardVM>();
        }

        /// <summary>
        /// The DataContext of the Login Window
        /// </summary>
        public LoginViewModel Login
        {
            get => ServiceLocator.Current.GetInstance<LoginViewModel>();
        }

        /// <summary>
        /// The DataContext of the Card Registration Page
        /// </summary>
        public CardRegistrationVM CardRegistration
        {
            get => ServiceLocator.Current.GetInstance<CardRegistrationVM>();
        }

        /// <summary>
        /// The DataContext of the Card Personalization Request Page
        /// </summary>
        public PersonalizationRequestVM PersonalizationRequest
        {
            get => ServiceLocator.Current.GetInstance<PersonalizationRequestVM>();
        }

        /// <summary>
        /// The DataContext of the Card Request Page
        /// </summary>
        public CardRequestsVM CardRequest
        {
            get => ServiceLocator.Current.GetInstance<CardRequestsVM>();
        }

        /// <summary>
        /// The DataContext of the Card Request Page
        /// </summary>
        public ReplaceCardRequestVM ReplaceCardRequest
        {
            get => ServiceLocator.Current.GetInstance<ReplaceCardRequestVM>();
        }

        public static void Cleanup()
        {
            SimpleIoc.Default.Reset();
        }
    }
}