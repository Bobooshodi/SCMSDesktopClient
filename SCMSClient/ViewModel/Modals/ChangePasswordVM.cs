using SCMSClient.Services.Interfaces;
using System.Security;

namespace SCMSClient.ViewModel
{
    public class ChangePasswordVM : BaseSettingsVM<IChangePasswordM>
    {
        private readonly IUserService userService;

        public ChangePasswordVM(IUserService _userService, IDinkeyDongleService _dongleService, ISettingsService _settingsService) :
            base(_settingsService, _dongleService)
        {
            userService = _userService;
        }

        public string OldPassword { get; set; }

        #region Command Methods

        protected override void Process(object obj)
        {
            MainObject = obj as IChangePasswordM;
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