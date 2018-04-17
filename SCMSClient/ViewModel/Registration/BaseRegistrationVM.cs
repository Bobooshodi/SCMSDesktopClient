using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using SCMSClient.Models;
using SCMSClient.ToastNotification;
using System;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public abstract class BaseRegistrationVM : ViewModelBase
    {
        private Cardholder cardholder;
        private bool loaderVisibility;
        protected readonly Toaster toaster = Toaster.Instance;

        protected virtual bool CanPerformAction { get => false; }

        protected BaseRegistrationVM()
        {
            GotoFirstPage = new RelayCommand(OpenFirstPage);
            GotoSecondPage = new RelayCommand(OpenSecondPage);
            PerformAction = new RelayCommand(ProcessAction, () => CanPerformAction);
        }

        public ICommand PerformAction { get; set; }
        public ICommand GotoFirstPage { get; set; }
        public ICommand GotoSecondPage { get; set; }

        protected MainWindowVM MainWindowVM
        {
            get => SimpleIoc.Default.GetInstance<MainWindowVM>();
        }

        public bool LoaderVisibility
        {
            get => loaderVisibility;
            set => Set(ref loaderVisibility, value, true);
        }

        public virtual Cardholder Cardholder
        {
            get
            {
                return cardholder ?? (cardholder = new Cardholder());
            }
            set => Set(ref cardholder, value, true);
        }

        private void OpenSecondPage()
        {
            if (Cardholder.UserType == SHCCardType.Employee)
            {
                MainWindowVM.ActivePage = new Uri("/Views/EmployeeRegistration.xaml", UriKind.RelativeOrAbsolute);
            }
            if (Cardholder.UserType == SHCCardType.Tenant)
            {
                MainWindowVM.ActivePage = new Uri("/Views/TenantRegistration.xaml", UriKind.RelativeOrAbsolute);
            }
        }

        private void OpenFirstPage()
        {
            MainWindowVM.ActivePage = new Uri("/Views/RegisterCardholder.xaml", UriKind.RelativeOrAbsolute);
        }

        protected string DisplayError(Func<bool> expression)
        {
            if (expression())
            {
                return "InputBorderHasError";
            }
            return "InputBorder";
        }

        protected abstract void ProcessAction();

        protected abstract bool ValidateFields();

        public override void Cleanup()
        {
            SimpleIoc.Default.Unregister<CardholderRegistrationVM>("new");
            SimpleIoc.Default.Unregister<EmployeeRegistrationVM>("new");
            SimpleIoc.Default.Unregister<TenantRegistrationVM>("new");
            SimpleIoc.Default.Unregister<VehicleRegistrationVM>("new");

            base.Cleanup();
        }
    }
}