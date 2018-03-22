using SCMSClient.ViewModel;
using System.Security;
using System.Windows;

namespace SCMSClient.Modals
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : System.Windows.Controls.UserControl, IChangePasswordM
    {
        private System.Windows.Controls.PasswordBox passwordBox;
        private System.Windows.Controls.Border border;
        private System.Windows.Controls.TextBlock textBlock;

        public ChangePassword()
        {
            InitializeComponent();

            // DataContext = SimpleIoc.Default.GetInstance<CardPersonalizationVM>();
        }

        public SecureString OldPassword => oldPassword.SecurePassword;

        public SecureString NewPassword => newPassword.SecurePassword;

        public SecureString ConfirmPassword => confirmPassword.SecurePassword;

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            GetSender(sender);

            if (passwordBox?.SecurePassword.Length < 1)
            {
                border.Style = (Style)FindResource("InputBorderHasError");
                textBlock.Text = "Please, Enter a Password";
                textBlock.Visibility = Visibility.Visible;
            }

            if (passwordBox?.Name == "confirmPassword")
            {
                if (newPassword.Password != confirmPassword.Password)
                {
                    newPasswordBorder.Style = (Style)FindResource("InputBorderHasError");
                    confirmPasswordBorder.Style = (Style)FindResource("InputBorderHasError");
                    newPasswordNotification.Text = "Passwords do not match ";
                    newPasswordNotification.Visibility = Visibility.Visible;
                    confirmPasswordNotification.Text = "Passwords do not match ";
                    confirmPasswordNotification.Visibility = Visibility.Visible;
                }
                else
                {
                    newPasswordBorder.Style = (Style)FindResource("InputBorder");
                    confirmPasswordBorder.Style = (Style)FindResource("InputBorder");
                    newPasswordNotification.Text = "";
                    newPasswordNotification.Visibility = Visibility.Collapsed;
                    confirmPasswordNotification.Text = "";
                    confirmPasswordNotification.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Password_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            GetSender(sender);

            if (System.Console.CapsLock)
            {
                border.Style = (Style)FindResource("InputBorderHasError");
                textBlock.Text = "Caps Lock Is On";
                textBlock.Visibility = Visibility.Visible;
            }
            else
            {
                border.Style = (Style)FindResource("InputBorder");
                textBlock.Text = string.Empty;
                textBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void GetSender(object sender)
        {
            switch (((System.Windows.Controls.PasswordBox)sender).Name)
            {
                case "oldPassword":
                    passwordBox = oldPassword;
                    border = oldPasswordBorder;
                    textBlock = oldPasswordNotification;
                    break;

                case "newPassword":
                    passwordBox = newPassword;
                    border = newPasswordBorder;
                    textBlock = newPasswordNotification;
                    break;

                case "confirmPassword":
                    passwordBox = confirmPassword;
                    border = confirmPasswordBorder;
                    textBlock = confirmPasswordNotification;
                    break;
            }
        }
    }
}