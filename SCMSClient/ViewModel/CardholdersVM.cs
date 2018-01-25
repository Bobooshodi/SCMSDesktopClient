using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Models;
using SCMSClient.Utilities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class CardholdersVM : ViewModelBase
    {
        #region Private Members

        private string cardholderFilterText;
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
            FilterCardholderCommand = new RelayCommand<object>(FilterCardholders);

            LoadAllCardholders();
        }

        #endregion

        #region Private Methods

        private bool CardholderSearchFilter(object obj)
        {
            var cardholder = obj as Cardholder;

            if (cardholder?.FullName?.IndexOf(CardholderFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || cardholder?.IdentificationNo?.IndexOf(CardholderFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            return false;
        }

        private void LoadAllCardholders()
        {
            AllCardholders = FilteredCardholders = new ObservableCollection<Cardholder>(RandomDataGenerator.Cardholders(10));


        }

        #endregion


        #region Public Properties

        public string CardholderFilterText
        {
            get => cardholderFilterText ?? string.Empty;
            set => Set(ref cardholderFilterText, value, true);
        }

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
            get
            {
                CardholdersCollection = CollectionViewSource.GetDefaultView(filteredCardholders);
                CardholdersCollection.Filter = CardholderSearchFilter;

                return filteredCardholders;
            }
            set => Set(ref filteredCardholders, value, true);
        }

        #endregion


        #region ICommands

        /// <summary>
        /// Command to create new Cardholder
        /// </summary>
        public ICommand CreateCommand { get; set; }
        public ICommand ViewCardholderCommand { get; set; }
        public ICommand FilterCardholderCommand { get; set; }

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

        private void FilterCardholders(object obj)
        {
            var filter = obj as string;

            var cardholders = AllCardholders
                .Where(ch => ch.Cards
                                .Any(c => c.CardType.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0))
                .ToList();

            FilteredCardholders = new ObservableCollection<Cardholder>(cardholders);

        }

        #endregion
    }
}
