using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using SCMSClient.Utilities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace SCMSClient.ViewModel
{
    /// <summary>
    /// Base Class for all Modal's Viewmodels
    /// This class is used to view details about a 
    /// single Selected Item in a List of Items
    /// </summary>
    /// <typeparam name="T">
    /// the <see cref="System.Type"/> of the Item Selected
    /// </typeparam>
    public abstract class BaseModalsVM<T> : ViewModelBase
    {
        #region Members Declaration

        private T selectedItem;
        protected IAbstractService<T> service;

        protected readonly Toaster toastManager = Toaster.Instance;
        protected virtual bool CanProcess
        {
            get
            {
                if (IsProcessing)
                    return false;

                return true;
            }
        }

        #endregion


        #region Default Constructor

        /// <summary>
        /// The default constructor Inheriteda and implemented by all Child classes
        /// </summary>
        protected BaseModalsVM(IAbstractService<T> _service)
        {
            service = _service;

            ProcessCommand = new RelayCommand(async () => await Process(), () => CanProcess);
            CloseCommand = new RelayCommand(CloseModal);
        }

        #endregion


        #region ICommands

        /// <summary>
        /// Command to Close the Active Modal
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// Command to Process any change to the <see cref="SelectedItem"/>
        /// </summary>
        public ICommand ProcessCommand { get; set; }

        #endregion


        #region Public Properties

        /// <summary>
        /// This is a Flag to show the state of the <see cref="Process"/> Method
        /// </summary>
        public bool IsProcessing { get; set; }

        /// <summary>
        /// The Property to bind to the View
        /// </summary>
        public T SelectedItem
        {
            get => selectedItem;
            set => Set(ref selectedItem, value, true);
        }

        #endregion


        #region inheritable Methods

        /// <summary>
        /// This Method runs the <see cref="ProcessLogic"/> Method in a new <see cref="Task"/> 
        /// when the <see cref="ProcessCommand"/> is invoked
        /// All Child classes must implemen their own logic
        /// </summary>
        protected virtual async Task Process()
        {
            // Check if the flag property is true (meaning the function is already running)
            if (IsProcessing)
            {
                toastManager.ShowErrorToast(Toaster.ErrorTitle, "Please wait for the current operation to complete before starting another one");
                return;
            }

            try
            {
                // Set the property flag to true to indicate we are running
                IsProcessing = true;

                await ProcessLogic();
            }
            catch (Exception e)
            {
                toastManager.ShowErrorToast(Toaster.ErrorTitle, e.Message);
            }
            finally
            {
                // Set the property flag back to false now it's finished
                IsProcessing = false;
            }
        }

        /// <summary>
        /// This is the Logic to execute when the <see cref="ProcessCommand"/> is invoked
        /// </summary>
        /// <returns></returns>
        protected abstract Task ProcessLogic();

        /// <summary>
        /// The Logic that is called when the <see cref="CloseCommand"/> is invoked
        /// the Child classes can override this Logic if there's a custom logic to
        /// close the modal
        /// </summary>
        protected virtual void CloseModal()
        {
            MessengerInstance.Send<UIElement>(null);
        }

        #endregion


        #region Member Methods

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
    }
}
