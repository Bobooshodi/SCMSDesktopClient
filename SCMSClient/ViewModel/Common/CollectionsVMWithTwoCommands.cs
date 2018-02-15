using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public abstract class CollectionsVMWithTwoCommands<T> : CollectionsVMWithOneCommand<T>
    {
        #region Members Declaration

        private ObservableCollection<T> filteredCollection;

        #endregion

        #region Default Constructor

        /// <summary>
        /// The default constructor inherited from the base class and should be implemented
        /// by all derived classes
        /// </summary>
        /// <param name="_service">
        /// the default Service class that manages the Type inferred from the Argument passed to the
        /// class
        /// </param>
        protected CollectionsVMWithTwoCommands(IAbstractService<T> _service) : base(_service: _service)
        {
            FilterCollectionsCommand = new RelayCommand<object>(FilterCollections);
        }

        #endregion

        #region ICommands

        /// <summary>
        /// This is the command to filter all the AllObjects Collection
        /// </summary>
        public ICommand FilterCollectionsCommand { get; set; }

        #endregion


        #region Member Methods

        /// <summary>
        /// This is a method thar runs on the initialization of the class
        /// to load all objects using the service class
        /// </summary>
        /// <returns></returns>
        protected override async Task LoadAll()
        {
            try
            {
                await RunMethodAsync(() =>
                {
                    if (AllObjects?.Count > 0)
                        AllObjects.Clear();

                    if (FilteredCollection?.Count > 0)
                        FilteredCollection.Clear();

                    var allObjects = service.GetAll() ?? new List<T>();
                    AllObjects = FilteredCollection = new ObservableCollection<T>(allObjects);
                }, () => IsBusy);
            }
            catch (Exception e)
            {
                toaster.ShowErrorToast(Toaster.ErrorTitle, e.Message);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// This is an <see cref="ObservableCollection{T}"/> of the Type inferred from the 
        /// Argument passed to the class.
        /// It holds the result of the Filtering of the AllObjects Collection
        /// </summary>
        public ObservableCollection<T> FilteredCollection
        {
            get
            {
                if (filteredCollection != null)
                {
                    AllObjectsCollection = CollectionViewSource.GetDefaultView(filteredCollection);
                    AllObjectsCollection.Filter = SearchFilter;
                }

                return filteredCollection;
            }
            set => Set(ref filteredCollection, value, true);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Abstract class to implement the Logic to ecexute when the <see cref="FilterCollectionsCommand"/>
        /// is invoked
        /// This method must be implemented in all Derived classes
        /// </summary>
        /// <param name="obj">
        /// an object passed on from the View
        /// </param>
        protected abstract void FilterCollections(object obj);

        #endregion
    }
}
