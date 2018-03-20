using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using SCMSClient.Utilities;
using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        #region Private Members

        private string username;
        private readonly Toaster toastManager = Toaster.Instance;
        private readonly IAuthenticationService authService;

        private readonly IDinkeyDongleService dongleService;
        private bool loaderVisibility;

        private bool CanLogin => !string.IsNullOrEmpty(Username);

        #endregion Private Members

        #region Default Constructor

        public LoginViewModel(IAuthenticationService _authService, IDinkeyDongleService _dongleService)
        {
            dongleService = _dongleService;
            authService = _authService;

            LoginCommand = new RelayCommand<object>(async (object obj) => await Login(obj), (obj) => CanLogin);
            NextPageCommand = new RelayCommand<object>(OpenNextPage);
        }

        #endregion Default Constructor

        #region Button Commands for the View

        public ICommand LoginCommand { get; set; }
        public ICommand NextPageCommand { get; set; }

        #endregion Button Commands for the View

        #region Public Properties

        /// <summary>
        /// Public Property to hold The User's
        /// Username
        /// </summary>
        public string Username
        {
            get => username;
            set => Set(ref username, value, true);
        }

        public bool LoaderVisibility
        {
            get => loaderVisibility;
            set => Set(ref loaderVisibility, value, true);
        }

        public string UsernameBorderStyle
        {
            get
            {
                if (DisplayError())
                {
                    return "InputBorder";
                }

                return "InputBorderHasError";
            }
        }

        public bool UsernameErrorTextVisibility
        {
            get => !DisplayError();
        }

        #endregion Public Properties

        #region Private Methods

        private void CheckPCValidity()
        {
            try
            {
                var mutexName = "";

                if (dongleService.IsDonglePresent())
                {
                    var allowedPc = false;

                    var data = dongleService.GetDongleData();

                    foreach (var pc in data.PCToBoind)
                    {
                        if (pc.Uid == mutexName)
                        {
                            allowedPc = true;
                            break;
                        }
                    }

                    if (!allowedPc)
                    {
                        throw new Exception("SCMS is not configured to work on this PC");
                    }
                }
                else
                {
                    throw new Exception("Dongle is not Present");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Not Allowed");

                Application.Current.Shutdown(0);
            }
        }

        private bool DisplayError()
        {
            if (Username?.Length < 1)
                return false;

            return true;
        }

        /// <summary>
        /// Just a Method to go to the next page
        /// should be removed before deployment
        /// </summary>
        /// <param name="obj">The Login window</param>
        private void OpenNextPage(object obj)
        {
            Application.Current.Properties["loggedInUser"] = new Models.User
            {
                Access_token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1bmlxdWVfbmFtZSI6ImR1YmEiLCJzdWIiOiJkdWJhIiwicm9sZSI6WyJBY2Nlc3NDb250cm9sTWFuYWdlcl9GaW5kIiwiQWNjZXNzQ29udHJvbFN5c3RlbV9DcmVhdGUiLCJBY2Nlc3NDb250cm9sU3lzdGVtX0RlbGV0ZSIsIkFjY2Vzc0NvbnRyb2xTeXN0ZW1fRmluZCIsIkFjY2Vzc0NvbnRyb2xTeXN0ZW1fVXBkYXRlIiwiQWNjZXNzR3JvdXBfQ3JlYXRlIiwiQWNjZXNzR3JvdXBfRGVsZXRlIiwiQWNjZXNzR3JvdXBfRmluZCIsIkFjY2Vzc0dyb3VwX0ZpbmREZXRhaWwiLCJBY2Nlc3NHcm91cF9VcGRhdGUiLCJBcHBNb2R1bGVfRmluZCIsIkFwcFJlc291cmNlc19GaW5kIiwiQXBwUmVzb3VyY2VzX01hbmFnZURlc2t0b3AiLCJBcHBSZXNvdXJjZXNfTWFuYWdlUG9ydGFsIiwiQnVzaW5lc3NVbml0X0NyZWF0ZSIsIkJ1c2luZXNzVW5pdF9EZWxldGUiLCJCdXNpbmVzc1VuaXRfRmluZCIsIkJ1c2luZXNzVW5pdF9GaW5kRGV0YWlsIiwiQnVzaW5lc3NVbml0X1VwZGF0ZSIsIkNhcmRTZXR0aW5nX0NyZWF0ZSIsIkNhcmRTZXR0aW5nX0RlbGV0ZSIsIkNhcmRTZXR0aW5nX0ZpbmQiLCJDYXJkU2V0dGluZ19VcGRhdGUiLCJDYXJkU3RhdHVzSGlzdG9yeV9DcmVhdGUiLCJDYXJkU3RhdHVzSGlzdG9yeV9EZXRhY2giLCJDYXJkU3RhdHVzSGlzdG9yeV9GaW5kIiwiQ2FyZFR5cGVfQ3JlYXRlIiwiQ2FyZFR5cGVfRGVsZXRlIiwiQ2FyZFR5cGVfRmluZCIsIkNhcmRUeXBlX0ZpbmREZXRhaWwiLCJDYXJkVHlwZV9VcGRhdGUiLCJDYXJkX0NyZWF0ZSIsIkNhcmRfRGVsZXRlIiwiQ2FyZF9GaW5kIiwiQ2FyZF9GaW5kRGV0YWlsIiwiQ2FyZF9VcGRhdGUiLCJGYWNpbGl0eV9DcmVhdGUiLCJGYWNpbGl0eV9EZWxldGUiLCJGYWNpbGl0eV9GaW5kIiwiRmFjaWxpdHlfRmluZERldGFpbCIsIkZhY2lsaXR5X1VwZGF0ZSIsIkxldmVsX0NyZWF0ZSIsIkxldmVsX0RlbGV0ZSIsIkxldmVsX0ZpbmQiLCJMZXZlbF9VcGRhdGUiLCJNYWluRmFjaWxpdHlfQ3JlYXRlIiwiTWFpbkZhY2lsaXR5X0RlbGV0ZSIsIk1haW5GYWNpbGl0eV9GaW5kIiwiTWFpbkZhY2lsaXR5X0ZpbmREZXRhaWwiLCJNYWluRmFjaWxpdHlfVXBkYXRlIiwiUGVybWFuZW50VXNlckNhcmRTdGF0dXNIaXN0b3J5X0NyZWF0ZSIsIlBlcm1hbmVudFVzZXJDYXJkU3RhdHVzSGlzdG9yeV9GaW5kIiwiUGVybWFuZW50VXNlclN0YXR1c0hpc3RvcnlfQ3JlYXRlIiwiUGVybWFuZW50VXNlclN0YXR1c0hpc3RvcnlfRmluZCIsIlBlcm1hbmVudFVzZXJfQ3JlYXRlIiwiUGVybWFuZW50VXNlcl9EZWxldGUiLCJQZXJtYW5lbnRVc2VyX0ZpbmQiLCJQZXJtYW5lbnRVc2VyX0ZpbmREZXRhaWwiLCJQZXJtYW5lbnRVc2VyX1VwZGF0ZSIsIlJlcG9ydF9CbGFja2xpc3RlZFZpc2l0b3JzIiwiUmVwb3J0X0NhcmROb3RSZXR1cm5lZCIsIlJlcG9ydF9WaXNpdERldGFpbHMiLCJSZXBvcnRfVmlzaXRvclRyYW5zYWN0aW9ucyIsIlJvbGVfQ3JlYXRlIiwiUm9sZV9EZWxldGUiLCJSb2xlX0ZpbmQiLCJSb2xlX0ZpbmREZXRhaWwiLCJSb2xlX1VwZGF0ZSIsIlVzZXJfQWN0aXZhdGUiLCJVc2VyX0NyZWF0ZSIsIlVzZXJfRGVhY3RpdmF0ZSIsIlVzZXJfRmluZCIsIlVzZXJfRmluZERldGFpbCIsIlVzZXJfVXBkYXRlIiwiVmlzaXREZXRhaWxfQ2hlY2tJbiIsIlZpc2l0RGV0YWlsX0NoZWNrT3V0IiwiVmlzaXREZXRhaWxfRmluZCIsIlZpc2l0RGV0YWlsX0ZpbmREZXRhaWwiLCJWaXNpdG9yU3RhdHVzSGlzdG9yeV9DcmVhdGUiLCJWaXNpdG9yU3RhdHVzSGlzdG9yeV9GaW5kIiwiVmlzaXRvcl9DcmVhdGUiLCJWaXNpdG9yX0ZpbmQiLCJWaXNpdG9yX1VwZGF0ZSJdLCJpc3MiOiJodHRwOi8vMTkyLjE2OC4wLjMzOjEwMDAxIiwiYXVkIjoiNjcxYjBkZjQwODdmNDZmYWI4ZjgxZmE5ODJlYjRlZDgiLCJleHAiOjE1MTY2MzM5MTQsIm5iZiI6MTUxNjYwMzkxNH0.DkDb4y_MmtGlkgEP5wGUNFWsNXhCfGnbgzZKBl_EIb4",
                Client_id = "desktop",
                Username = "Duba"
            };

            var nextWindow = new MainWindow
            {
                WindowState = WindowState.Maximized
            };
            nextWindow.Show();

            var window = obj as Windows.Login;
            window.Close();
        }

        /// <summary>
        /// Obtains the Username and password from the interface, and sends
        /// it to the server for athentication
        /// </summary>
        /// <param name="obj">an object refrence to the Login Window</param>
        /// <returns></returns>
        private async Task Login(object obj)
        {
            if (obj == null)
            {
                toastManager.ShowInformationToast(Toaster.InformationTitle, "Page is Initializing, Please wait a moment and try again");
                return;
            }

            try
            {
                var loginWindow = obj as IHavePassword;

                if (loginWindow.UserPassword.Length < 1)
                    throw new Exception("Please, Enter a Password");

                LoaderVisibility = true;

                var loggedInUser = await Task.Run(() => authService.Login(Username, loginWindow.UserPassword.Unsecure()));

                LoaderVisibility = false;

                if (loggedInUser != null)
                {
                    Application.Current.Properties["loggedInUser"] = loggedInUser;

                    var nextWindow = new MainWindow
                    {
                        WindowState = WindowState.Maximized
                    };
                    nextWindow.Show();

                    var window = obj as Windows.Login;
                    window.Close();
                }
                else
                {
                    throw new Exception("Something went wrong, please try again");
                }
            }
            catch (Exception e)
            {
                toastManager.ShowErrorToast(Toaster.ErrorTitle, e.Message);
            }
        }

        #endregion Private Methods
    }

    public interface IHavePassword
    {
        SecureString UserPassword { get; }
    }
}