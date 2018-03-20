using GalaSoft.MvvmLight.Ioc;
using SCMSClient.ViewModel;
using System.Windows.Controls;

namespace SCMSClient.Views
{
    /// <summary>
    /// Interaction logic for RegisterCardholder.xaml
    /// </summary>
    public partial class RegisterCardholder : Page
    {
        public RegisterCardholder()
        {
            InitializeComponent();

            DataContext = SimpleIoc.Default.GetInstance<CardholderRegistrationVM>("new");
        }
    }
}