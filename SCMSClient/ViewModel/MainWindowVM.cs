using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Models;
using SCMSClient.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        #region Private Members

        private Uri activePage;
        private UIElement activeModal;
        private User loggedInUser;

        #endregion


        #region Default Constructor

        public MainWindowVM()
        {
            ToastNotification.Toaster.Refresh();

            if (Application.Current?.Properties?["loggedInUser"] != null)
                loggedInUser = Application.Current.Properties["loggedInUser"] as User;

            MessengerInstance.Register<UIElement>(this, ChangeModal);

            MessengerInstance.Register<CardholderDetails>(this, OpenCardholderDetails);

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
            get => activePage ?? new Uri("/Views/Dashboard.xaml", UriKind.RelativeOrAbsolute);
            set => Set(ref activePage, value, true);
        }

        public UIElement ActiveModal
        {
            get => activeModal;
            set => Set(ref activeModal, value, true);
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
                    ActivePage = new Uri("/Views/MainRequestPage.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "inventory":
                    ActivePage = new Uri("/Views/CardInventory.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "cardholders":
                    ActivePage = new Uri("/Views/CardholderList.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "cardholder-details":
                    ActivePage = new Uri("/Views/CardholderDetails.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "settings":
                    break;
            }
        }

        #endregion


        #region Messenger Methods

        private void ChangeModal(UIElement modal)
        {
            ActiveModal = modal;
        }

        private void OpenCardholderDetails(CardholderDetails obj)
        {
            Navigate("cardholder-details");
        }

        #endregion
    }
}
