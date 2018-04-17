using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Win32;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class CardholderRegistrationVM : BaseRegistrationVM
    {
        private readonly ICardTypeService cardTypeService;

        public CardholderRegistrationVM(ICardTypeService _cardTypeService)
        {
            cardTypeService = _cardTypeService;
            DateOfBirth = DateTime.Now;

            ChooseFileCommand = new RelayCommand(ChooseFile);

            LoadAll().ConfigureAwait(false);
        }

        private void ChooseFile()
        {
            try
            {
                // Displays an OpenFileDialog so the user can select a Cursor.
                var dialog = new OpenFileDialog
                {
                    Filter = "PNG Files|*.png|All files (*.*)|*.*|JPG Files|*.jpg|JPEG Files|*.jpeg",
                    Title = "Select an image for this Employee"
                };

                if (dialog.ShowDialog().Value)
                {
                    ImagePath = dialog.FileName;
                    GetImageFromPath(dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                toaster.ShowErrorToast(Toaster.ErrorTitle, ex.Message);
            }
        }

        public ICommand ChooseFileCommand { get; set; }

        public string ImagePath { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PrefferredName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string IdNumber { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public SHCCardType UserType { get; set; }

        public List<string> Countries { get; set; }

        public List<SHCCardType> UserTypes { get; set; }

        public List<string> Genders
        {
            get => new List<string> { "Male", "Female" };
        }

        #region Error

        public string FirstNameBorderStyle
        {
            get
            {
                return DisplayError(() => FirstNameErrorTextVisibility);
            }
        }

        public string LastNameBorderStyle
        {
            get
            {
                return DisplayError(() => LastNameErrorTextVisibility);
            }
        }

        public string PreferredNameBorderStyle
        {
            get
            {
                return DisplayError(() => string.IsNullOrEmpty(FirstName));
            }
        }

        public string DOBBorderStyle
        {
            get
            {
                return DisplayError(() => DOBErrorTextVisibility);
            }
        }

        public string IdNumberBorderStyle
        {
            get
            {
                return DisplayError(() => IdNumberErrorTextVisibility);
            }
        }

        public string AddressBorderStyle
        {
            get
            {
                return DisplayError(() => AddressErrorTextVisibility);
            }
        }

        public string StateBorderStyle
        {
            get
            {
                return DisplayError(() => StateErrorTextVisibility);
            }
        }

        public string CityBorderStyle
        {
            get
            {
                return DisplayError(() => CityErrorTextVisibility);
            }
        }

        public string PhoneBorderStyle
        {
            get => DisplayError(() => PhoneErrorTextVisibility);
        }

        public string EmailBorderStyle
        {
            get
            {
                try
                {
                    return DisplayError(() => EmailErrorTextVisibility);
                }
                catch
                {
                    return "InputBorderHasError";
                }
            }
        }

        public bool FirstNameErrorTextVisibility
        {
            get => FirstName?.Length == 0;
        }

        public bool LastNameErrorTextVisibility
        {
            get => LastName?.Length == 0;
        }

        public bool PrefferedNameErrorTextVisibility
        {
            get => false;
        }

        public bool DOBErrorTextVisibility
        {
            get => DateOfBirth >= DateTime.Now;
        }

        public bool IdNumberErrorTextVisibility
        {
            get => IdNumber?.Length == 0;
        }

        public bool AddressErrorTextVisibility
        {
            get => Address?.Length == 0;
        }

        public bool StateErrorTextVisibility
        {
            get => State?.Length == 0;
        }

        public bool CityErrorTextVisibility
        {
            get => City?.Length == 0;
        }

        public bool EmailErrorTextVisibility
        {
            get
            {
                if (Email != null)
                {
                    try
                    {
                        var addr = new System.Net.Mail.MailAddress(Email);

                        return Email.Length == 0 || addr.Address != Email;
                    }
                    catch
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public bool PhoneErrorTextVisibility
        {
            get
            {
                if (Phone != null)
                {
                    return Phone.Length == 0 || !double.TryParse(Phone, out double intValue);
                }

                return false;
            }
        }

        protected override bool CanPerformAction => ValidateFields();

        #endregion Error

        private async Task LoadAll()
        {
            try
            {
                await Task.Run(() =>
                {
                    UserTypes = new List<SHCCardType>
                    {
                        SHCCardType.Employee,
                        SHCCardType.Individual,
                        SHCCardType.Strata,
                        SHCCardType.Tenant
                    };
                });
            }
            catch (Exception ex)
            {
                toaster.ShowErrorToast(Toaster.ErrorTitle, ex.Message);
            }
        }

        private void GetImageFromPath(string path)
        {
            var imageBytes = File.ReadAllBytes(path);
            var imageString = Convert.ToBase64String(imageBytes);
        }

        protected override void ProcessAction()
        {
            if (UserType == SHCCardType.Employee)
            {
                var dc = SimpleIoc.Default.GetInstance<EmployeeRegistrationVM>("new");
                dc.Cardholder = CoupleEmployeeDetails();
                MainWindowVM.ActivePage = new Uri("/Views/EmployeeRegistration.xaml", UriKind.RelativeOrAbsolute);
            }
            if (UserType == SHCCardType.Tenant)
            {
                var dc = SimpleIoc.Default.GetInstance<TenantRegistrationVM>("new");
                dc.Cardholder = CoupleTenantDetails();
                MainWindowVM.ActivePage = new Uri("/Views/TenantRegistration.xaml", UriKind.RelativeOrAbsolute);
            }
        }

        protected override bool ValidateFields()
        {
            return !FirstNameErrorTextVisibility && !LastNameErrorTextVisibility
            && !PrefferedNameErrorTextVisibility && !DOBErrorTextVisibility && !IdNumberErrorTextVisibility
            && !AddressErrorTextVisibility && !StateErrorTextVisibility && !CityErrorTextVisibility
            && !EmailErrorTextVisibility && !PhoneErrorTextVisibility;
        }

        private Employee CoupleEmployeeDetails()
        {
            return new Employee
            {
                FirstName = FirstName,
                LastName = LastName,
                IdentificationNo = IdNumber,
                DateOfBirth = DateOfBirth,
                Email = Email,
                City = City,
                State = State,
                UserType = UserType,
                Phone = Phone,
                Address = Address,
                Gender = Cardholder.Gender,
                IdentificationType = Cardholder.IdentificationType,
                Country = Cardholder.Country,
                WorkEmail = Cardholder.WorkEmail,
                WorkPhone = Cardholder.WorkPhone
            };
        }

        private Tenant CoupleTenantDetails()
        {
            return new Tenant
            {
                FirstName = FirstName,
                LastName = LastName,
                IdentificationNo = IdNumber,
                DateOfBirth = DateOfBirth,
                Email = Email,
                City = City,
                State = State,
                UserType = UserType,
                Phone = Phone,
                Address = Address,
                Gender = Cardholder.Gender,
                IdentificationType = Cardholder.IdentificationType,
                Country = Cardholder.Country,
                WorkEmail = Cardholder.WorkEmail,
                WorkPhone = Cardholder.WorkPhone
            };
        }
    }
}