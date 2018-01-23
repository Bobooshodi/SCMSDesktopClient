using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        #region Private Members
        private Uri activePage;

        private User loggedInUser =
            Application.Current.Properties["loggedInUser"] as User;

        #endregion


        #region Default Constructor

        public MainWindowVM()
        {
            ActivePage = new Uri("/Views/Dashboard.xaml", UriKind.RelativeOrAbsolute);

            NavigationCommand = new RelayCommand<object>(Navigate);
        }

        #endregion


        #region Commands

        public ICommand NavigationCommand { get; set; }

        #endregion


        #region Public Properties

        public string LoggedInUserName => $"Welcome {loggedInUser.Username}";

        public Uri ActivePage
        {
            get => activePage;
            set => Set(ref activePage, value, true);
        }


        #endregion


        #region Private Methods

        private void Navigate(object obj)
        {
            switch (obj as string)
            {
                case "dashboard":
                    ActivePage = new Uri("/Views/Dashboard.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "requests":
                    ActivePage = new Uri("/Views/CardRequest.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "inventory":
                    ActivePage = new Uri("/Views/CardInventory.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "cardholders":
                    ActivePage = new Uri("/Views/CardholderList.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "settings":
                    break;
            }
        }

        #endregion
    }
}
