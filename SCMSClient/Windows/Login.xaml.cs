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

        private void userPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (System.Console.CapsLock)
            {
                notificationText.Content = "Caps Lock Is On";
            }
            else
            {
                notificationText.Content = string.Empty;
            }
        }
    }
}
