using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SCMSClient.ViewModel
{
    /// <summary>
    /// View Model Logic for the Card Registration Modal
    /// </summary>
    public class CardRegistrationVM : BaseModalsVM<Card>
    {
        #region Private Members

        private CardType selectedCardType;
        private ObservableCollection<CardType> cardTypes;
        private ObservableCollection<CardVendor> cardVendors;
        private CardVendor selectedCardVendor;
        private ICardService cardService;
        private readonly ICardTypeService cardTypeService;
        private readonly ICardVendorService cardVendorService;

        #endregion


        #region Default Constructor

        /// <summary>
        /// This Class' implementation of the Base Class' constructor
        /// </summary>
        /// <param name="_cardService"></param>
        /// <param name="_cardTypeService"></param>
        /// <param name="_cardVendorService"></param>
        public CardRegistrationVM(ICardService _cardService, ICardTypeService _cardTypeService,
            ICardVendorService _cardVendorService)
        {
            cardService = _cardService;
            cardTypeService = _cardTypeService;
            cardVendorService = _cardVendorService;

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

        /// <summary>
        /// This Method Loads all the Objects and Collections needed
        /// </summary>
        private void LoadAll()
        {
            Task.Run(() =>
                {
                    CardVendors = new ObservableCollection<CardVendor>(cardVendorService.GetAll());
                    CardTypes = new ObservableCollection<CardType>(cardTypeService.GetAll());
                });
        }

        /// <summary>
        /// this funcrion provides a way to release
        /// memory once the class is destroyed
        /// </summary>
        public override void Cleanup()
        {
            SelectedCardType = null;
            SelectedCardVendor = null;
            cardService = null;

            base.Cleanup();
        }

        #endregion


        #region Command Methods

        /// <summary>
        /// This Class' Implementation of the Process Logic defined in the Base Class
        /// </summary>
        protected override void Process()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
