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
    public class PersonalizationRequestVM : ViewModelBase
    {
        #region Private Members

        private string requestsFilterText;
        private SOAPersonalizationRequest selectedRequest;
        private ObservableCollection<SOAPersonalizationRequest> allRequests;

        #endregion


        #region Default Constructor

        public PersonalizationRequestVM()
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
            AllRequests = new ObservableCollection<SOAPersonalizationRequest>(RandomDataGenerator.PersonalizationRequests(10));
        }

        private bool RequestSearchFilter(object obj)
        {
            var request = obj as SOAPersonalizationRequest;

            return request.Cardholder.IndexOf(RequestsFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request.IdentificationNo.IndexOf(RequestsFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request.RequestId.IndexOf(RequestsFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request.ContractNo.IndexOf(RequestsFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request.CardInventoryNo.IndexOf(RequestsFilterText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        #endregion


        #region Public Properties

        public string RequestsFilterText
        {
            get => requestsFilterText ?? string.Empty;
            set => Set(ref requestsFilterText, value, true);
        }

        public SOAPersonalizationRequest SelectedRequest
        {
            get => selectedRequest;
            set => Set(ref selectedRequest, value, true);
        }

        public ObservableCollection<SOAPersonalizationRequest> AllRequests
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
