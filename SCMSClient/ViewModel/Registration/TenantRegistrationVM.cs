using GalaSoft.MvvmLight.Ioc;
using SCMSClient.Models;
using SCMSClient.ToastNotification;
using SCMSClient.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCMSClient.ViewModel
{
    public class TenantRegistrationVM : BaseRegistrationVM
    {
        public TenantRegistrationVM()
        {
            LoadAll().ConfigureAwait(false);
        }

        protected override bool CanPerformAction => ValidateFields();

        public string ContractId { get; set; }

        public string Company { get; set; }

        public string Agreement { get; set; }

        public List<Building> Buildings { get; set; }

        #region Error

        public string IdBorderStyle
        {
            get
            {
                return DisplayError(() => IdErrorTextVisibility);
            }
        }

        public string CompanyBorderStyle
        {
            get
            {
                return DisplayError(() => CompanyErrorTextVisibility);
            }
        }

        public string AgreementBorderStyle
        {
            get
            {
                return DisplayError(() => AgreementErrorTextVisibility);
            }
        }

        public bool IdErrorTextVisibility
        {
            get => ContractId?.Length == 0;
        }

        public bool CompanyErrorTextVisibility
        {
            get => Company?.Length == 0;
        }

        public bool AgreementErrorTextVisibility
        {
            get => Agreement?.Length == 0;
        }

        #endregion Error

        private async Task LoadAll()
        {
            try
            {
                await Task.Run(() =>
                {
                    Buildings = RandomDataGenerator.Buildings(10);
                });
            }
            catch (Exception ex)
            {
                toaster.ShowErrorToast(Toaster.ErrorTitle, ex.Message);
            }
        }

        protected override void ProcessAction()
        {
            var dc = SimpleIoc.Default.GetInstance<VehicleRegistrationVM>("new");
            dc.Cardholder = CoupleCardholderDetails();
            MainWindowVM.ActivePage = new Uri("/Views/VehicleRegistration.xaml", UriKind.RelativeOrAbsolute);
        }

        private Tenant CoupleCardholderDetails()
        {
            var tenant = Cardholder as Tenant;

            return tenant;
        }

        protected override bool ValidateFields()
        {
            return true;
        }
    }
}