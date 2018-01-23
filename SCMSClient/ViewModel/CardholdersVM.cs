using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class CardholdersVM : ViewModelBase
    {
        #region Private Members

        private ObservableCollection<Cardholder> allCardholders;
        private ObservableCollection<Cardholder> filteredCardholders;
        private Cardholder selectedCardholder;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CardholdersVM()
        {
            CreateCommand = new RelayCommand(CreateCardholder);
            ViewCardholderCommand = new RelayCommand(ViewCardholder);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// This Property Holds any Selected Cardholder in a collection
        /// </summary>
        public Cardholder SelectedCardholder
        {
            get => selectedCardholder;
            set => Set(ref selectedCardholder, value, true);
        }

        /// <summary>
        /// Public Collection of Cardholders to bind to the view
        /// </summary>
        public ObservableCollection<Cardholder> AllCardholders
        {
            get => allCardholders;
            set => Set(ref allCardholders, value, true);
        }

        /// <summary>
        /// holds the Cardholders filtered from the original list
        /// </summary>
        public ObservableCollection<Cardholder> FilteredCardholders
        {
            get => filteredCardholders;
            set => Set(ref filteredCardholders, value, true);
        }

        #endregion

        #region Dummy Data

        #endregion

        #region ICommands

        /// <summary>
        /// Command to create new Cardholder
        /// </summary>
        public ICommand CreateCommand { get; set; }
        public ICommand ViewCardholderCommand { get; set; }

        #endregion

        #region ICollectionViews

        private ICollectionView CardholdersCollection;

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

        private void ViewCardholder()
        {

        }

        #endregion
    }
}
