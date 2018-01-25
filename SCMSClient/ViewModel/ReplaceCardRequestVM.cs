using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Models;
using SCMSClient.Utilities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class ReplaceCardRequestVM : ViewModelBase
    {
        #region Private Members

        private string requestsFilterText;
        private SOAReplaceCardRequest selectedRequest;
        private ObservableCollection<SOAReplaceCardRequest> allRequests;

        #endregion


        #region Default Constructor

        public ReplaceCardRequestVM()
        {
            ProcessCommand = new RelayCommand(ProcessRequest);

            LoadAllRequests();
        }

        #endregion


        #region ICommand

        public ICommand ProcessCommand { get; set; }

        #endregion

        #region ICollectionViews

        public ICollectionView RequestsCollection { get; set; }

        #endregion


        #region Private Methods

        private void LoadAllRequests()
        {
            AllRequests = new ObservableCollection<SOAReplaceCardRequest>(RandomDataGenerator.ReplaceCardRequests(10));
        }

        private bool RequestSearchFilter(object obj)
        {
            var request = obj as SOAReplaceCardRequest;

            return request?.Cardholder?.IndexOf(RequestsFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.CardId?.IndexOf(RequestsFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.ReplacedBy?.IndexOf(RequestsFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.ReplacedCardId?.IndexOf(RequestsFilterText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        #endregion


        #region Public Properties

        public string RequestsFilterText
        {
            get => requestsFilterText ?? string.Empty;
            set => Set(ref requestsFilterText, value, true);
        }

        public SOAReplaceCardRequest SelectedRequest
        {
            get => selectedRequest;
            set => Set(ref selectedRequest, value, true);
        }

        public ObservableCollection<SOAReplaceCardRequest> AllRequests
        {
            get
            {
                RequestsCollection = CollectionViewSource.GetDefaultView(allRequests);
                RequestsCollection.Filter = RequestSearchFilter;

                return allRequests;
            }
            set => Set(ref allRequests, value, true);
        }

        #endregion


        #region Command Methods

        private void ProcessRequest()
        {
        }

        #endregion
    }
}
