using SCMSClient.Modals;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SCMSClient.ViewModel
{
    public class CardsListVM : CollectionsVMWithTwoCommands<Card>
    {
        public CardsListVM(ICardService _service, IDinkeyDongleService _dongleService) : base(_service: _service, _dongleService: _dongleService)
        {
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

        protected override void Process()
        {
            var modal = new VerifyCard();

            MessengerInstance.Send<UIElement>(modal);
        }

        protected override bool SearchFilter(object obj)
        {
            var card = obj as Card;

            if (card?.CardInventoryNo?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || card?.BatchNo?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            return false;
        }
    }
}