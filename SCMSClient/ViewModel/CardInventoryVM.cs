using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SCMSClient.ViewModel
{
    public class CardInventoryVM : CollectionsVMWithTwoCommands<Card>
    {
        public override bool IsBusy { get; set; }

        #region Default Constructors

        public CardInventoryVM(ICardService _cardService, IDinkeyDongleService _dongleService) : base(_service: _cardService, _dongleService: _dongleService)
        {
        }

        #endregion Default Constructors

        #region Private Methods

        protected override bool SearchFilter(object obj)
        {
            var cardholder = obj as Card;

            if (cardholder?.CardInventoryNo?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || cardholder?.BatchNo?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            return false;
        }

        #endregion Private Methods

        #region Command Methods

        /// <summary>
        /// This Method is called when the RegisterCommand Action is invoked
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

            ChangeStyle(filter);
        }

        #endregion Command Methods
    }
}