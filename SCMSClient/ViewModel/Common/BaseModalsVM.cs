using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.ToastNotification;
using System;
using System.Collections.Generic;
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
        protected BaseModalsVM()
        {
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
        /// The Logic that's called when the <see cref="ProcessCommand"/> is invoked
        /// All Child classes must implemen their own logic
        /// </summary>
        protected virtual async Task Process()
        {
            try
            {
                await RunCommandAsync(() => ProcessLogic());
            }
            catch (Exception e)
            {
                toastManager.ShowErrorToast(Toaster.ErrorTitle, e.Message);
            }
        }

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

        protected async Task RunCommandAsync(Func<Task> action)
        {
            // Check if the flag property is true (meaning the function is already running)
            if (IsProcessing)
                throw new InvalidOperationException("Please wait for the current operation to complete before starting another one");

            try
            {
                // Set the property flag to true to indicate we are running
                IsProcessing = true;

                await action();
            }
            catch
            {
                throw;
            }
            finally
            {
                // Set the property flag back to false now it's finished
                IsProcessing = false;
            }
        }

        protected async Task RunMethodAsync(Action action)
        {
            try
            {
                // Set the property flag to true to indicate we are running
                IsProcessing = true;

                await Task.Run(action);
            }
            catch
            {
                throw;
            }
            finally
            {
                // Set the property flag back to false now it's finished
                IsProcessing = false;
            }
        }

        protected async Task RunMethodAsync(List<Action> actions)
        {
            try
            {
                var caughtExceptions = new List<Exception>();

                // Set the property flag to true to indicate we are running
                IsProcessing = true;

                foreach (var action in actions)
                {
                    try
                    {
                        await Task.Run(action);
                    }
                    catch (Exception e)
                    {
                        caughtExceptions.Add(e);
                        continue;
                    }
                    finally
                    {
                        caughtExceptions.ForEach(e => throw e);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                // Set the property flag back to false now it's finished
                IsProcessing = false;
            }
        }

        #endregion
    }
}
