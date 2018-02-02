using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SCMSClient.ViewModel
{
    public class CardholdersVM : CollectionsVMWithThreeCommand<Cardholder>
    {
        private ICardholderService cardholderService;

        #region Default Constructor

        public CardholdersVM(ICardholderService _cardholderService)
        {
            cardholderService = _cardholderService;

            LoadAll();
        }

        #endregion

        #region Private Methods

        protected override bool SearchFilter(object obj)
        {
            var cardholder = obj as Cardholder;

            if (cardholder?.FullName?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || cardholder?.IdentificationNo?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            return false;
        }

        protected override void LoadAll()
        {
            try
            {
                //AllObjects = FilteredCollection = new ObservableCollection<Cardholder>(RandomDataGenerator.Cardholders(10));

                Task.Run(() => AllObjects = FilteredCollection = new ObservableCollection<Cardholder>(cardholderService.GetAll()));
            }
            catch (Exception e)
            {
                toaster.ShowErrorToast(Toaster.ErrorTitle, e.Message);
            }
        }

        #endregion


        #region Command Methods

        /// <summary>
        /// This Method is called when the <see cref="CreateCommand"/> Action is invoked
        /// The Methos Opens a new page to create the Cardholder
        /// </summary>
        protected override void Process()
        {
            throw new NotImplementedException();
        }

        protected override void ViewObject()
        {

        }

        protected override void FilterCollections(object obj)
        {
            var filter = obj as string;

            var cardholders = AllObjects
                .Where(ch => ch.Cards
                                .Any(c => c.CardType.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0))
                .ToList();

            FilteredCollection = new ObservableCollection<Cardholder>(cardholders);
        }

        #endregion
    }
}
