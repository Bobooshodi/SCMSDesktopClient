using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class CardholdersVM : ViewModelBase
    {
        #region Private Members
        private ObservableCollection<Cardholder> cardholders;
        private Cardholder selectedCardholder;
        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CardholdersVM()
        {
            CreateCommand = new RelayCommand(CreateCardholder);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// This Property Holds any Selected Cardholder in a collection
        /// </summary>
        public Cardholder SelectedCardholder
        {
            get { return selectedCardholder; }
            set { Set(ref selectedCardholder, value, true); }
        }


        /// <summary>
        /// Public Collection of Cardholders to bind to the view
        /// </summary>
        public ObservableCollection<Cardholder> Cardholders
        {
            get { return cardholders; }
            set { Set(ref cardholders, value, true); }
        }

        #endregion

        #region ICommands

        /// <summary>
        /// Command to create new Cardholder
        /// </summary>
        public ICommand CreateCommand { get; set; }

        #endregion

        #region Command Methods

        /// <summary>
        /// This Method is called when the <see cref="CreateCommand"/> Action is invoked
        /// The Methos Opens a new page to create the Cardholder
        /// </summary>
        private void CreateCardholder()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
