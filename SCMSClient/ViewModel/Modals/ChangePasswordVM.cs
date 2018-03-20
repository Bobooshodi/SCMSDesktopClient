using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Services.Interfaces;
using System.Security;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class ChangePasswordVM : ViewModelBase
    {
        private readonly IUserService userService;

        public ChangePasswordVM(IUserService _userService)
        {
            userService = _userService;

            CloseModalCommand = new RelayCommand(CloseModal);
            ChangePasswordCommand = new RelayCommand<object>(ChangePassword);
        }

        public ICommand CloseModalCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }


        public string OldPassword { get; set; }

        #region Command Methods

        private void ChangePassword(object page)
        {
            var model = page as IChangePasswordM;
        }

        private void CloseModal()
        {
            MessengerInstance.Send<System.Windows.UIElement>(null);
        }


        #endregion
    }

    public interface IChangePasswordM
    {
        SecureString OldPassword { get; }
        SecureString NewPassword { get; }
        SecureString ConfirmPassword { get; }
    }
}
