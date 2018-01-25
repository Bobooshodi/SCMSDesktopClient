using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class CardRegistrationVM : ViewModelBase
    {
        #region Private Members

        private Card selectedCard;
        private CardType selectedCardType;
        private ObservableCollection<CardType> cardTypes;
        private ObservableCollection<CardVendor> cardVendors;
        private CardVendor selectedCardVendor;
        private ICardService cardService;

        #endregion


        #region Default Constructor

        public CardRegistrationVM(Card _selectedCard)
        {
            cardService = SimpleIoc.Default.GetInstance<ICardService>();

            SelectedCard = _selectedCard;

            RegisterCommand = new RelayCommand(RegisterCard);
            CancelCommand = new RelayCommand(CloseModal);

            LoadAll();
        }

        #endregion


        #region Public ICommands

        public ICommand RegisterCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        #endregion


        #region Public Properties

        public Card SelectedCard
        {
            get => selectedCard;
            set => Set(ref selectedCard, value, true);
        }

        public CardType SelectedCardType
        {
            get => selectedCardType;
            set => Set(ref selectedCardType, value, true);
        }

        public CardVendor SelectedCardVendor
        {
            get => selectedCardVendor;
            set => Set(ref selectedCardVendor, value, true);
        }

        public ObservableCollection<CardType> CardTypes
        {
            get => cardTypes;
            set => Set(ref cardTypes, value, true);
        }

        public ObservableCollection<CardVendor> CardVendors
        {
            get => cardVendors;
            set => Set(ref cardVendors, value, true);
        }

        #endregion


        #region Private Methods

        private void LoadAll()
        {
            CardVendors = new ObservableCollection<CardVendor>(RandomDataGenerator.CardVendors(4));
            CardTypes = new ObservableCollection<CardType>(RandomDataGenerator.CardTypes());
        }

        public override void Cleanup()
        {
            SelectedCard = null;
            SelectedCardType = null;
            SelectedCardVendor = null;
            cardService = null;
            Application.Current.Properties["selectedCard"] = null;

            base.Cleanup();
        }

        #endregion


        #region Command Methods

        private void RegisterCard()
        {
            throw new NotImplementedException();
        }

        private void CloseModal()
        {
            MessengerInstance.Send<UIElement>(null);
        }

        #endregion
    }
}
