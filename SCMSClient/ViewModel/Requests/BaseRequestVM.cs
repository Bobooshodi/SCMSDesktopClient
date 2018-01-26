using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public abstract class BaseRequestVM<T> : ViewModelBase
    {
        #region Private Members

        private string filterText;
        private T selectedRequest;
        private ObservableCollection<T> allRequests;

        #endregion


        public BaseRequestVM()
        {
            ProcessCommand = new RelayCommand(ProcessRequest);

            LoadAllRequests();
        }


        #region ICollectionViews

        public ICollectionView RequestsCollection { get; set; }

        #endregion


        #region ICommand

        public ICommand ProcessCommand { get; set; }

        #endregion


        #region Private Methods

        protected abstract void LoadAllRequests();

        protected abstract bool RequestSearchFilter(object obj);

        #endregion


        #region Public Properties

        public string FilterText
        {
            get => filterText ?? string.Empty;
            set
            {
                Set(ref filterText, value, true);

                RequestsCollection?.Refresh();
            }
        }

        public T SelectedRequest
        {
            get => selectedRequest;
            set => Set(ref selectedRequest, value, true);
        }

        public ObservableCollection<T> AllRequests
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

        protected abstract void ProcessRequest();

        #endregion
    }
}
