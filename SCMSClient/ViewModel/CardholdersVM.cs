using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using System;
using System.Collections.Generic;
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

            LoadAll().ConfigureAwait(false);
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

        protected override async Task LoadAll()
        {
            try
            {
                await Task.Run(() =>
                {
                    var allCardholders = cardholderService.GetAll() ?? new List<Cardholder>();
                    AllObjects = FilteredCollection = new ObservableCollection<Cardholder>(allCardholders);
                });
            }
            catch (Exception e)
            {
                toaster.ShowErrorToast(Toaster.ErrorTitle, e.Message);
            }
        }

        #endregion


        #region Command Methods

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
