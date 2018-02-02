using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.ToastNotification;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public abstract class CollectionsVMWithOneCommand<T> : ViewModelBase
    {
        #region Private Members

        protected string filterText;
        private T selectedObject;
        private ObservableCollection<T> allObjects;
        protected readonly Toaster toaster = new Toaster();

        #endregion


        #region Default Constructor

        protected CollectionsVMWithOneCommand()
        {
            ProcessCommand = new RelayCommand(Process);

            // LoadAll();
        }

        #endregion


        #region ICollectionViews

        /// <summary>
        /// A <see cref="CollectionView"/> to hold a view of the List of 
        /// Requests for easy filtering and sorting
        /// </summary>
        public ICollectionView AllObjectsCollection { get; set; }

        #endregion


        #region ICommand

        /// <summary>
        /// an <see cref="ICommand"/> to bind to the view to process the 
        /// button click
        /// </summary>
        public ICommand ProcessCommand { get; set; }

        #endregion


        #region Private Methods

        /// <summary>
        /// This method Loads all the objects to display in the List
        /// </summary>
        protected abstract void LoadAll();

        /// <summary>
        /// This Method contains the logic to needed by the <see cref="AllObjectsCollection"/> to
        /// filter the Displayed List
        /// </summary>
        /// <param name="obj">
        /// represents an object in the <see cref="AllObjects"/> List
        /// </param>
        /// <returns>
        /// returns true if the filter condition passes or
        /// false if the filter condition fails
        /// </returns>
        protected abstract bool SearchFilter(object obj);

        #endregion


        #region Public Properties

        public virtual string FilterText
        {
            get => filterText ?? string.Empty;
            set
            {
                Set(ref filterText, value, true);

                AllObjectsCollection?.Refresh();
            }
        }

        public T SelectedObject
        {
            get => selectedObject;
            set => Set(ref selectedObject, value, true);
        }

        public ObservableCollection<T> AllObjects
        {
            get
            {
                if (allObjects != null)
                {
                    AllObjectsCollection = CollectionViewSource.GetDefaultView(allObjects);
                    AllObjectsCollection.Filter = SearchFilter;
                }

                return allObjects;
            }
            set => Set(ref allObjects, value, true);
        }

        #endregion


        #region Command Methods

        /// <summary>
        /// contains the Logic to process the <see cref="SelectedObject"/>
        /// </summary>
        protected abstract void Process();

        #endregion
    }
}
