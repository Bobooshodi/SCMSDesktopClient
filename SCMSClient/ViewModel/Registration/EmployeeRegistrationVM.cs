using GalaSoft.MvvmLight.Ioc;
using SCMSClient.Models;
using SCMSClient.ToastNotification;
using SCMSClient.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCMSClient.ViewModel
{
    public class EmployeeRegistrationVM : BaseRegistrationVM
    {
        public EmployeeRegistrationVM()
        {
            LoadAll().ConfigureAwait(false);
        }

        protected override bool CanPerformAction => ValidateFields();

        public string EmployeeId { get; set; }

        public string Company { get; set; }

        public string Department { get; set; }

        public string Designation { get; set; }

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

        public string DepartmentBorderStyle
        {
            get => DisplayError(() => DepartmentErrorTextVisibility);
        }

        public string DesignationBorderStyle
        {
            get
            {
                return DisplayError(() => DesignationErrorTextVisibility);
            }
        }

        public bool IdErrorTextVisibility
        {
            get => EmployeeId?.Length == 0;
        }

        public bool CompanyErrorTextVisibility
        {
            get => Company?.Length == 0;
        }

        public bool DepartmentErrorTextVisibility
        {
            get => Department?.Length == 0;
        }

        public bool DesignationErrorTextVisibility
        {
            get => Designation?.Length == 0;
        }

        #endregion Error

        protected override void ProcessAction()
        {
            var dc = SimpleIoc.Default.GetInstance<VehicleRegistrationVM>("new");
            dc.Cardholder = CoupleCardholderDetails();
            MainWindowVM.ActivePage = new Uri("/Views/VehicleRegistration.xaml", UriKind.RelativeOrAbsolute);
        }

        private Employee CoupleCardholderDetails()
        {
            var employee = Cardholder as Employee;

            employee.EmployeeId = EmployeeId;
            // employee.Company = Company;
            employee.Designation = Designation;
            employee.Buildings = Buildings;

            return employee;
        }

        protected override bool ValidateFields()
        {
            return true;
        }

        private async Task LoadAll()
        {
            try
            {
                await Task.Run(() => Buildings = RandomDataGenerator.Buildings(20));
            }
            catch (Exception ex)
            {
                toaster.ShowErrorToast(Toaster.ErrorTitle, ex.Message);
            }
        }
    }
}