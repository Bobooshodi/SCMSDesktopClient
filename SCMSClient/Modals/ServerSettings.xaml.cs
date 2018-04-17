using GalaSoft.MvvmLight.Ioc;
using SCMSClient.ViewModel;
using System.Windows.Controls;

namespace SCMSClient.Modals
{
    /// <summary>
    /// Interaction logic for ServerSettings.xaml
    /// </summary>
    public partial class ServerSettings : UserControl
    {
        public ServerSettings(bool fromWithinApp)
        {
            InitializeComponent();

            var dc = SimpleIoc.Default.GetInstance<ServerSettingsVM>();
            dc.IsCalledFromWithinApp = fromWithinApp;

            DataContext = dc;
        }
    }
}