using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using System;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class RequestsVM : ViewModelBase
    {
        #region Private Members

        private Uri activePage;
        private readonly Toaster toaster = Toaster.Instance;
        private readonly IDinkeyDongleService dongleService;

        #endregion Private Members

        #region Default Constructor

        public RequestsVM(IDinkeyDongleService _dongleService)
        {
            ChangeStyle("personalization");

            dongleService = _dongleService;

            NavigationCommand = new RelayCommand<object>(Navigate);
        }

        #endregion Default Constructor

        #region Commands

        public ICommand NavigationCommand { get; set; }

        #endregion Commands

        #region Public Properties

        public string PersonalisationStyle { get; set; }

        public string ReplacementStyle { get; set; }

        public string BlacklistStyle { get; set; }

        public string DistributionStyle { get; set; }

        public Uri ActivePage
        {
            get => activePage ?? (activePage = new Uri("/Views/PersonalizationRequest.xaml", UriKind.RelativeOrAbsolute));
            set => Set(ref activePage, value, true);
        }

        #endregion Public Properties

        #region Private Methods

        private void Navigate(object obj)
        {
            try
            {
                if (!dongleService.IsDonglePresent())
                {
                    throw new Exception("Please, Chack the Dongle and try again");
                }

                var page = obj as string;

                ChangeStyle(page);

                switch (page)
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
            catch (Exception ex)
            {
                toaster.ShowErrorToast(Toaster.ErrorTitle, ex.Message);
            }
        }

        protected void ChangeStyle(string caller)
        {
            switch (caller)
            {
                case "personalization":
                    PersonalisationStyle = "ComplementaryBrush";
                    BlacklistStyle = DistributionStyle = ReplacementStyle = "MarkerBrush";
                    break;

                case "replacement":
                    ReplacementStyle = "ComplementaryBrush";
                    BlacklistStyle = DistributionStyle = PersonalisationStyle = "MarkerBrush";
                    break;

                case "blacklist":
                    BlacklistStyle = "ComplementaryBrush";
                    ReplacementStyle = DistributionStyle = PersonalisationStyle = "MarkerBrush";
                    break;

                case "distribution":
                    DistributionStyle = "ComplementaryBrush";
                    ReplacementStyle = BlacklistStyle = PersonalisationStyle = "MarkerBrush";
                    break;

                default:
                    PersonalisationStyle = BlacklistStyle = DistributionStyle = ReplacementStyle = "MarkerBrush";
                    break;
            }
        }

        #endregion Private Methods
    }
}