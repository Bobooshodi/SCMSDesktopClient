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
        private readonly IDinkeyDongleService dongleService;

        public ChangePasswordVM(IUserService _userService, IDinkeyDongleService _dongleService)
        {
            userService = _userService;
            dongleService = _dongleService;

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

        #endregion Command Methods
    }

    public interface IChangePasswordM
    {
        SecureString OldPassword { get; }
        SecureString NewPassword { get; }
        SecureString ConfirmPassword { get; }
    }
}