using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class RequestsVM : ViewModelBase
    {
        #region Private Members

        private Uri activePage;

        #endregion


        #region Default Constructor

        public RequestsVM()
        {
            NavigationCommand = new RelayCommand<object>(Navigate);
        }

        #endregion


        #region Commands

        public ICommand NavigationCommand { get; set; }

        #endregion


        #region Public Properties

        public Uri ActivePage
        {
            get => activePage ?? new Uri("/Views/CardRequest.xaml", UriKind.RelativeOrAbsolute);
            set => Set(ref activePage, value, true);
        }

        #endregion


        #region Private Methods

        private void Navigate(object obj)
        {
            switch (obj as string)
            {
                case "personalization":
                    ActivePage = new Uri("/Views/PersonalizationRequest.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "replacement":
                    ActivePage = new Uri("/Views/ReplaceCard.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "blacklist":
                    ActivePage = new Uri("/Views/BlacklistRequest.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "distribution":
                    ActivePage = new Uri("/Views/CardRequest.xaml", UriKind.RelativeOrAbsolute);
                    break;
            }
        }

        #endregion
    }
}
