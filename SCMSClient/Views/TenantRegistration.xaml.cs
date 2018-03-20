using GalaSoft.MvvmLight.Ioc;
using SCMSClient.ViewModel;
using System.Windows.Controls;

namespace SCMSClient.Views
{
    /// <summary>
    /// Interaction logic for TenantRegistration.xaml
    /// </summary>
    public partial class TenantRegistration : Page
    {
        public TenantRegistration()
        {
            InitializeComponent();

            DataContext = SimpleIoc.Default.GetInstance<TenantRegistrationVM>("new");
        }
    }
}