using GalaSoft.MvvmLight.Ioc;
using SCMSClient.ViewModel;
using System.Windows.Controls;

namespace SCMSClient.Views
{
    /// <summary>
    /// Interaction logic for EmployeeRegistration.xaml
    /// </summary>
    public partial class EmployeeRegistration : Page
    {
        public EmployeeRegistration()
        {
            InitializeComponent();

            DataContext = SimpleIoc.Default.GetInstance<EmployeeRegistrationVM>("new");
        }
    }
}