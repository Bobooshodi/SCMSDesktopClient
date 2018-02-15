using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using SCMSClient.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public abstract class CollectionsVMWithOneCommand<T> : ViewModelBase
    {
        #region Members Declaration

        private T selectedObject;
        protected readonly IAbstractService<T> service;
        protected string filterText;
        private ObservableCollection<T> allObjects;
        protected readonly Toaster toaster = Toaster.Instance;


        #endregion


        #region Default Constructor

        /// <summary>
        /// Default constructor for this class and should be implemented
        /// by all derived classes
        /// </summary>
        /// <param name="_service">
        /// the default Service class that manages the Type inferred from the Argument passed to the
        /// class
        /// </param>
        protected CollectionsVMWithOneCommand(IAbstractService<T> _service)
        {
            service = _service;

            ProcessCommand = new RelayCommand(Process);

            LoadAll().ConfigureAwait(false);
        }

        #endregion


        #region ICollectionViews

        /// <summary>
        /// A <see cref="CollectionView"/> to hold a view of the List of 
        /// Requests for easy filtering and sorting
        /// </summary>
        public ICollectionView AllObjectsCollection { get; set; }

        #endregion


        #region ICommands

        /// <summary>
        /// an <see cref="ICommand"/> to bind to the view to process the 
        /// button click
        /// </summary>
        public ICommand ProcessCommand { get; set; }

        #endregion


        #region Member Methods

        /// <summary>
        /// This method Loads all the objects to display in the List
        /// </summary>
        protected virtual async Task LoadAll()
        {
            try
            {
                await RunMethodAsync(() =>
                {
                    var allrequests = service.GetAll() ?? new List<T>();
                    AllObjects = new ObservableCollection<T>(allrequests);
                }, () => IsBusy);

            }
            catch (Exception e)
            {
                toaster.ShowErrorToast(Toaster.ErrorTitle, e.Message);
            }
        }

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

        /// <summary>
        /// This Method Runs the Method <paramref name="action"/> Passed to in in a
        /// New <see cref="Task"/> to avoid blocking the current Thread and updates the
        /// flag <paramref name="isRunning"/> passed to it to true or false to indicate 
        /// the state of the operation
        /// </summary>
        /// <param name="action">
        /// The Method to be run
        /// </param>
        /// <param name="isRunning">
        /// Flag to hold the state of the Operation
        /// </param>
        /// <returns></returns>
        protected async Task RunMethodAsync(Action action, Expression<Func<bool>> isRunning = null)
        {
            try
            {
                // Set the property flag to true to indicate we are running
                isRunning?.SetPropertyValue(true);

                await Task.Run(action);
            }
            catch
            {
                throw;
            }
            finally
            {
                // Set the property flag back to false now it's finished
                isRunning?.SetPropertyValue(false);
            }
        }

        #endregion


        #region Public Properties

        public virtual bool IsBusy { get; set; }

        /// <summary>
        /// Text to hold the value of the text to filter the List of <see cref="AllObjects"/>
        /// </summary>
        public virtual string FilterText
        {
            get => filterText ?? string.Empty;
            set
            {
                Set(ref filterText, value, true);

                AllObjectsCollection?.Refresh();
            }
        }

        /// <summary>
        /// This is an object in The <see cref="AllObjects"/> Collection that has been 
        /// selected by the user
        /// </summary>
        public T SelectedObject
        {
            get => selectedObject;
            set => Set(ref selectedObject, value, true);
        }

        /// <summary>
        /// This is a collection of all the objects of the Type inferred from the Argument
        /// passed to the class
        /// </summary>
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
