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

using GalaSoft.MvvmLight;
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

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                RegisterDesigntimeServices();
            }
            else
            {
                // Create run time view services and models
                RegisterRuntimeServices();
            }
            RegisterAllViewModels();
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
        /// The DataContext of the Verify Card modal
        /// </summary>
        public VerifyCardVM VerifyCard
        {
            get => ServiceLocator.Current.GetInstance<VerifyCardVM>();
        }

        /// <summary>
        /// The DataContext of the Cards List view
        /// </summary>
        public CardsListVM CardsList
        {
            get => ServiceLocator.Current.GetInstance<CardsListVM>();
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

        /// <summary>
        /// The DataContext of the Cardholder Registration Page
        /// </summary>
        public CardholderRegistrationVM CardholderRegistration
        {
            get => ServiceLocator.Current.GetInstance<CardholderRegistrationVM>();
        }

        /// <summary>
        /// The DataContext of the Employee Registration Page
        /// </summary>
        public EmployeeRegistrationVM EmployeeRegistration
        {
            get => ServiceLocator.Current.GetInstance<EmployeeRegistrationVM>();
        }

        /// <summary>
        /// The DataContext of the Tenant Registration Page
        /// </summary>
        public TenantRegistrationVM TenantRegistration
        {
            get => ServiceLocator.Current.GetInstance<TenantRegistrationVM>();
        }

        /// <summary>
        /// The DataContext of the Tenant Registration Page
        /// </summary>
        public VehicleRegistrationVM VehicleRegistration
        {
            get => ServiceLocator.Current.GetInstance<VehicleRegistrationVM>();
        }

        /// <summary>
        /// The DataContext of the AddVehicle Modal
        /// </summary>
        public AddVehicleVM AddVehicles
        {
            get => ServiceLocator.Current.GetInstance<AddVehicleVM>();
        }

        /// <summary>
        /// The DataContext of the AddBuilding Modal
        /// </summary>
        public AddBuildingVM AddBuilding
        {
            get => ServiceLocator.Current.GetInstance<AddBuildingVM>();
        }

        /// <summary>
        /// The DataContext of the AddCarPark Modal
        /// </summary>
        public AddCarParkVM AddCarPark
        {
            get => ServiceLocator.Current.GetInstance<AddCarParkVM>();
        }

        /// <summary>
        /// The DataContext of the Change Password Modal
        /// </summary>
        public ChangePasswordVM ChangePassword
        {
            get => ServiceLocator.Current.GetInstance<ChangePasswordVM>();
        }

        /// <summary>
        /// The DataContext of the Server Settings Modal
        /// </summary>
        public ServerSettingsVM ServerSettings
        {
            get => ServiceLocator.Current.GetInstance<ServerSettingsVM>();
        }

        public static void Cleanup()
        {
            SimpleIoc.Default.Reset();
        }

        public static void RegisterAllViewModels()
        {
            SimpleIoc.Default.Register<RequestsVM>();
            SimpleIoc.Default.Register<DashboardVM>();
            SimpleIoc.Default.Register<CardsListVM>();
            SimpleIoc.Default.Register<VerifyCardVM>();
            SimpleIoc.Default.Register<AddCarParkVM>();
            SimpleIoc.Default.Register<MainWindowVM>();
            SimpleIoc.Default.Register<AddVehicleVM>();
            SimpleIoc.Default.Register<CardholdersVM>();
            SimpleIoc.Default.Register<AddBuildingVM>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CardRequestsVM>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<CardInventoryVM>();
            SimpleIoc.Default.Register<ServerSettingsVM>();
            SimpleIoc.Default.Register<ChangePasswordVM>();
            SimpleIoc.Default.Register<CardReplacementVM>();
            SimpleIoc.Default.Register<SystemOperatorsVM>();
            SimpleIoc.Default.Register<CardRegistrationVM>();
            SimpleIoc.Default.Register<CardDistributionVM>();
            SimpleIoc.Default.Register<BlacklistRequestVM>();
            SimpleIoc.Default.Register<CardholderDetailsVM>();
            SimpleIoc.Default.Register<TenantRegistrationVM>();
            SimpleIoc.Default.Register<ReplaceCardRequestVM>();
            SimpleIoc.Default.Register<CardPersonalizationVM>();
            SimpleIoc.Default.Register<VehicleRegistrationVM>();
            SimpleIoc.Default.Register<EmployeeRegistrationVM>();
            SimpleIoc.Default.Register<CardholderRegistrationVM>();
            SimpleIoc.Default.Register<PersonalizationRequestVM>();
        }

        public static void RegisterRuntimeServices()
        {
            SimpleIoc.Default.Register<IUserService, UserService>();
            SimpleIoc.Default.Register<ICardService, CardService>();
            SimpleIoc.Default.Register<ITenantService, TenantService>();
            SimpleIoc.Default.Register<ICarParkService, CarParkService>();
            SimpleIoc.Default.Register<IVehicleService, VehicleService>();
            SimpleIoc.Default.Register<IBuildingService, BuildingService>();
            SimpleIoc.Default.Register<IEmployeeService, EmployeeService>();
            SimpleIoc.Default.Register<ISettingsService, SettingsService>();
            SimpleIoc.Default.Register<ICardTypeService, CardTypeService>();
            SimpleIoc.Default.Register<IHTTPService, HTTPRequestService>();
            SimpleIoc.Default.Register<ICardVendorService, CardVendorService>();
            SimpleIoc.Default.Register<ICardholderService, CardholderService>();
            SimpleIoc.Default.Register<ICardReaderService, SL600ReaderService>();
            SimpleIoc.Default.Register<ICardRequestService, CardRequestService>();
            SimpleIoc.Default.Register<IDinkeyDongleService, DinkeyDongleService>();
            SimpleIoc.Default.Register<IAuthenticationService, AuthenticationService>();
            SimpleIoc.Default.Register<ICardReplacementService, CardReplacementService>();
            SimpleIoc.Default.Register<IPersonalizationRequestService, PersonalizationRequestService>();
        }

        public static void RegisterDesigntimeServices()
        {
        }
    }
}