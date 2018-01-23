using SCMSClient.ViewModel;
using System.Security;
using System.Windows;

namespace SCMSClient.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window, IHavePassword
    {
        public Login()
        {
            InitializeComponent();
        }

        public SecureString UserPassword => userPassword.SecurePassword;
    }
}
