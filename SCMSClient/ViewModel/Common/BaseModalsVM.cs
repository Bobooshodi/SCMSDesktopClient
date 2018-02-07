using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
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
        #region Private Members

        private T selectedItem;

        #endregion


        #region Default Constructor

        /// <summary>
        /// The default constructor Inheriteda and implemented by all Child classes
        /// </summary>
        protected BaseModalsVM()
        {
            ProcessCommand = new RelayCommand(Process);
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
        protected abstract void Process();

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
    }
}
