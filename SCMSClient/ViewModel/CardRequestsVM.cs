using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SCMSClient.Models;
using SCMSClient.Utilities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class CardRequestsVM : ViewModelBase
    {
        #region Private Members

        private string requestsFilterText;
        private SOACardRequest selectedRequest;
        private ObservableCollection<SOACardRequest> allRequests;

        #endregion


        #region Default Constructor

        public CardRequestsVM()
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
            AllRequests = new ObservableCollection<SOACardRequest>(RandomDataGenerator.CardRequests(10));
        }

        private bool RequestSearchFilter(object obj)
        {
            var request = obj as SOACardRequest;

            return request?.RequestedBy?.IndexOf(RequestsFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.CardType.Name?.IndexOf(RequestsFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.RequestId?.IndexOf(RequestsFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.BusinessUnit?.IndexOf(RequestsFilterText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        #endregion


        #region Public Properties

        public string RequestsFilterText
        {
            get => requestsFilterText ?? string.Empty;
            set => Set(ref requestsFilterText, value, true);
        }

        public SOACardRequest SelectedRequest
        {
            get => selectedRequest;
            set => Set(ref selectedRequest, value, true);
        }

        public ObservableCollection<SOACardRequest> AllRequests
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
