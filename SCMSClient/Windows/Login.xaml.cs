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
                //loginBorder.Style = (Style)FindResource("InputBorderHasError");
                notificationText.Text = "Caps Lock Is On";
                notificationText.Visibility = Visibility.Visible;
            }
            else
            {
                loginBorder.Style = (Style)FindResource("InputBorder");
                notificationText.Text = string.Empty;
                notificationText.Visibility = Visibility.Collapsed;
            }
        }

        private void userPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (userPassword.SecurePassword.Length < 1)
            {
                loginBorder.Style = (Style)FindResource("InputBorderHasError");
                notificationText.Text = "Please, Enter a Password";
                notificationText.Visibility = Visibility.Visible;
            }
        }
    }
}
