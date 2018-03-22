using GalaSoft.MvvmLight;

namespace SCMSClient.Models
{
    public class User : BaseModel
    {
        private string _password, _username, _tokenType, _clientId,
            _refreshToken, _accessToken, _grantType;

        public int Expires_in { get; set; }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username == value)
                    return;

                Set(() => Username, ref _username, value);
                RaisePropertyChanged(() => Username);
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password == value)
                    return;

                Set(() => Password, ref _password, value);
                RaisePropertyChanged(() => Password);
            }
        }

        public string Token_type
        {
            get
            {
                return _tokenType;
            }
            set
            {
                if (_tokenType == value)
                    return;

                Set(() => Token_type, ref _tokenType, value);
                RaisePropertyChanged(() => Token_type);
            }
        }

        public string Client_id
        {
            get
            {
                return _clientId;
            }
            set
            {
                if (_clientId == value)
                    return;

                Set(() => Client_id, ref _clientId, value);
                RaisePropertyChanged(() => Client_id);
            }
        }

        public string Refresh_token
        {
            get
            {
                return _refreshToken;
            }
            set
            {
                if (_refreshToken == value)
                    return;

                Set(() => Refresh_token, ref _refreshToken, value);
                RaisePropertyChanged(() => Refresh_token);
            }
        }

        public string Access_token
        {
            get
            {
                return _accessToken;
            }
            set
            {
                Set(() => Access_token, ref _accessToken, value);

                RaisePropertyChanged(() => Access_token);
            }
        }

        public string Grant_type
        {
            get
            {
                return _grantType;
            }
            set
            {
                Set(() => Grant_type, ref _grantType, value);

                RaisePropertyChanged(() => Grant_type);
            }
        }
    }

    public class PasswordChangeModel : ObservableObject
    {
        private string oldPassword, newPassword, confirmPassword;

        public string OldPassword
        {
            get
            {
                return oldPassword;
            }
            set
            {
                Set(() => OldPassword, ref oldPassword, value);

                RaisePropertyChanged(() => OldPassword);
            }
        }

        public string NewPassword
        {
            get
            {
                return newPassword;
            }
            set
            {
                Set(() => NewPassword, ref newPassword, value);

                RaisePropertyChanged(() => NewPassword);
            }
        }

        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }
            set
            {
                Set(() => ConfirmPassword, ref confirmPassword, value);

                RaisePropertyChanged(() => ConfirmPassword);
            }
        }
    }
}