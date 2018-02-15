using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using System;
using System.Collections.Generic;
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
        private readonly ICardTypeService cardTypeService;
        private readonly ICardVendorService cardVendorService;

        /// <summary>
        /// This is the Logic to Enable or Disable the Process button on the view
        /// </summary>
        protected override bool CanProcess
        {
            get
            {
                if (SelectedCardType == null || SelectedCardVendor == null)
                    return false;

                return true;
            }
        }

        #endregion


        #region Default Constructor

        /// <summary>
        /// This Class' implementation of the Base Class' constructor
        /// </summary>
        /// <param name="_cardTypeService"></param>
        /// <param name="_cardVendorService"></param>
        public CardRegistrationVM(ICardService _cardService, ICardTypeService _cardTypeService,
            ICardVendorService _cardVendorService) : base(_service: _cardService)
        {
            cardTypeService = _cardTypeService;
            cardVendorService = _cardVendorService;

            LoadAll().ConfigureAwait(false);
        }

        #endregion


        #region Public Properties

        /// <summary>
        /// This holds the value of the Cardtype Selected from the ComboBox
        /// </summary>
        public CardType SelectedCardType
        {
            get => selectedCardType;
            set => Set(ref selectedCardType, value, true);
        }

        /// <summary>
        /// This holds the value of the CardVendor Selected from the CardVendors
        /// ComboBox
        /// </summary>
        public CardVendor SelectedCardVendor
        {
            get => selectedCardVendor;
            set => Set(ref selectedCardVendor, value, true);
        }

        /// <summary>
        /// This is the Collection Bound to the CardTypes ComboBox
        /// </summary>
        public ObservableCollection<CardType> CardTypes
        {
            get => cardTypes;
            set => Set(ref cardTypes, value, true);
        }

        /// <summary>
        /// This is the Collection Bound to the CardVendors ComboBox
        /// </summary>
        public ObservableCollection<CardVendor> CardVendors
        {
            get => cardVendors;
            set => Set(ref cardVendors, value, true);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// this funcrion provides a way to release
        /// memory once the class is destroyed
        /// </summary>
        public override void Cleanup()
        {
            SelectedCardType = null;
            SelectedCardVendor = null;

            base.Cleanup();
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// This Method Loads all the Objects and Collections needed
        /// </summary>
        private async Task LoadAll()
        {
            try
            {
                await RunMethodAsync(() =>
                {
                    var allCardTypes = cardTypeService.GetAll() ?? new List<CardType>();
                    CardTypes = new ObservableCollection<CardType>(allCardTypes);
                }, () => IsProcessing);

                await RunMethodAsync(() =>
                {
                    var allCardVendors = cardVendorService.GetAll() ?? new List<CardVendor>();
                    CardVendors = new ObservableCollection<CardVendor>(allCardVendors);
                }, () => IsProcessing);
            }
            catch (Exception e)
            {
                toastManager.ShowErrorToast(Toaster.ErrorTitle, e.Message);
            }
        }

        #endregion


        #region Command Methods

        /// <summary>
        /// This Class' Implementation of the Process Logic defined in the Base Class
        /// </summary>
        protected override async Task ProcessLogic()
        {
            await Task.Delay(15000);

            toastManager.ShowSuccessToast(Toaster.SuccessTitle, "Succss!!!");
        }

        #endregion
    }
}
