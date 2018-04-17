using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using System.Windows;

namespace SCMSClient.ViewModel
{
    public class ServerSettingsVM : BaseSettingsVM<Server>
    {
        private double doubleValue;
        private int protocolIndex;

        public ServerSettingsVM(IDinkeyDongleService _dongleService, ISettingsService _settingsService) :
            base(_settingsService, _dongleService)
        {
        }

        public int ProtocolIndex
        {
            get => (int)MainObject.Protocol;
            set
            {
                Set(ref protocolIndex, value, true);
                MainObject.Protocol = (ConnectionProtocol)value;
            }
        }

        protected override bool CanProcess
        {
            get
            {
                return !string.IsNullOrEmpty(MainObject.IpAddress) && !string.IsNullOrEmpty(MainObject.Port)
                    && double.TryParse(MainObject.Port, out doubleValue) && MainObject.Port.Length < 6;
            }
        }

        protected override void LoadSetting()
        {
            MainObject = AppSettings.RemoteServer ?? new Server();
        }

        protected override void Process(object obj)
        {
            try
            {
                AppSettings = LoadAppSettings();

                AppSettings.RemoteServer = MainObject;

                Save(AppSettings);

                toaster.ShowSuccessToast(Toaster.SuccessTitle, "Server settings Saved successfully");

                if (IsCalledFromWithinApp)
                {
                    CloseModal();

                    MessageBox.Show("You will be Logged out for these changes to take effect \r\n please login again", "Information");

                    settingsService.LogOutUser();
                }
                else
                {
                    CloseModal();
                }
            }
            catch (System.Exception ex)
            {
                toaster.ShowErrorToast(Toaster.ErrorTitle, ex.Message);

                Utilities.ErrorLogger.LogError(@"Unable to save Server Settings \r\n" + ex, Utilities.ErrorType.APPLICATION_ERROR);

                MessageBox.Show("Unable to Set Server Settings, This Software will close now");

                Application.Current.Shutdown();
            }
        }
    }
}