using GalaSoft.MvvmLight.Ioc;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using System.Collections.ObjectModel;

namespace SCMSClient.ViewModel
{
    public class CardRegistrationVM : BaseModalsVM<Card>
    {
        #region Private Members

        private CardType selectedCardType;
        private ObservableCollection<CardType> cardTypes;
        private ObservableCollection<CardVendor> cardVendors;
        private CardVendor selectedCardVendor;
        private ICardService cardService;

        #endregion


        #region Default Constructor

        public CardRegistrationVM(Card _selectedCard) : base(_selectedItem: _selectedCard)
        {
            cardService = SimpleIoc.Default.GetInstance<ICardService>();

            LoadAll();
        }

        #endregion


        #region Public Properties

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
            SelectedCardType = null;
            SelectedCardVendor = null;
            cardService = null;

            base.Cleanup();
        }

        #endregion


        #region Command Methods

        protected override void Process()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
