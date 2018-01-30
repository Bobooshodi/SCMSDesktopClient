using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SCMSClient.ViewModel
{
    public class CardInventoryVM : CollectionsVMWithThreeCommand<Card>
    {
        #region Private Members

        private readonly ICardService cardService;

        #endregion


        #region Default Constructors

        public CardInventoryVM(ICardService _cardService)
        {
            cardService = _cardService;
        }

        #endregion


        #region Private Methods

        protected override bool SearchFilter(object obj)
        {
            var cardholder = obj as Card;

            if (cardholder?.CardInventoryNo?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || cardholder?.BatchNo?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void LoadAll()
        {
            AllObjects = FilteredCollection = new ObservableCollection<Card>(RandomDataGenerator.Cards(10));

        }

        #endregion


        #region Command Methods

        /// <summary>
        /// This Method is called when the <see cref="RegisterCommand"/> Action is invoked
        /// The Method Opens a new page to create the Card
        /// </summary>
        protected override void Process()
        {
            var modal = new Modals.CardRegistration(SelectedObject);
            MessengerInstance.Send<UIElement>(modal);
        }

        protected override void FilterCollections(object obj)
        {
            var filter = obj as string;

            var cards = AllObjects
                        .Where(c => c.CardType.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0)
                        .ToList();

            FilteredCollection = new ObservableCollection<Card>(cards);

        }

        protected override void ViewObject()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
