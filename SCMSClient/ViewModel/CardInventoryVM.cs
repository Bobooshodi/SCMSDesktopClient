using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class CardInventoryVM : ViewModelBase
    {
        #region Private Members
        private Card selectedCard;
        private ObservableCollection<Card> allCards;
        private string cardFilterText;
        private ObservableCollection<Card> filteredCards;
        private readonly ICardService cardService;

        #endregion


        #region Default Constructors

        public CardInventoryVM(ICardService _cardService)
        {
            cardService = _cardService;

            RegisterCommand = new RelayCommand(RegisterCard);
            FilterCardsCommand = new RelayCommand<object>(FilterCards);

            LoadAllCards();
        }

        #endregion


        #region Private Methods

        private bool CardSearchFilter(object obj)
        {
            var cardholder = obj as Card;

            if (cardholder?.CardInventoryNo?.IndexOf(CardFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || cardholder?.BatchNo?.IndexOf(CardFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LoadAllCards()
        {
            AllCards = FilteredCards = new ObservableCollection<Card>(RandomDataGenerator.Cards(10));

        }

        #endregion


        #region Public Properties

        public string CardFilterText
        {
            get => cardFilterText ?? string.Empty;
            set => Set(ref cardFilterText, value, true);
        }

        /// <summary>
        /// This Property Holds any Selected Cardholder in a collection
        /// </summary>
        public Card SelectedCard
        {
            get => selectedCard;
            set => Set(ref selectedCard, value, true);
        }

        /// <summary>
        /// Public Collection of Cardholders to bind to the view
        /// </summary>
        public ObservableCollection<Card> AllCards
        {
            get => allCards;
            set => Set(ref allCards, value, true);
        }

        /// <summary>
        /// holds the Cardholders filtered from the original list
        /// </summary>
        public ObservableCollection<Card> FilteredCards
        {
            get
            {
                CardsCollection = CollectionViewSource.GetDefaultView(filteredCards);
                CardsCollection.Filter = CardSearchFilter;

                return filteredCards;
            }
            set => Set(ref filteredCards, value, true);
        }

        #endregion


        #region ICollectionViews

        private ICollectionView CardsCollection;

        #endregion


        #region ICommands

        /// <summary>
        /// Command to create new Card
        /// </summary>
        public ICommand RegisterCommand { get; set; }
        public ICommand ViewCardCommand { get; set; }
        public ICommand FilterCardsCommand { get; set; }

        #endregion


        #region Command Methods

        /// <summary>
        /// This Method is called when the <see cref="RegisterCommand"/> Action is invoked
        /// The Method Opens a new page to create the Card
        /// </summary>
        private void RegisterCard()
        {
            var modal = new Modals.CardRegistration(SelectedCard);
            MessengerInstance.Send<UIElement>(modal);
        }

        private void FilterCards(object obj)
        {
            var filter = obj as string;

            var cards = AllCards
                        .Where(c => c.CardType.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0)
                        .ToList();

            FilteredCards = new ObservableCollection<Card>(cards);

        }

        #endregion
    }
}
