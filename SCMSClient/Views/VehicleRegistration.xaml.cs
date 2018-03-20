using GalaSoft.MvvmLight.Ioc;
using SCMSClient.ViewModel;
using System.Windows.Controls;

namespace SCMSClient.Views
{
    /// <summary>
    /// Interaction logic for VehicleRegistration.xaml
    /// </summary>
    public partial class VehicleRegistration : Page
    {
        public VehicleRegistration()
        {
            InitializeComponent();

            DataContext = SimpleIoc.Default.GetInstance<VehicleRegistrationVM>("new");
        }
    }
}