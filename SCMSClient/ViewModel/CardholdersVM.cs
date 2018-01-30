using SCMSClient.Models;
using SCMSClient.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SCMSClient.ViewModel
{
    public class CardholdersVM : CollectionsVMWithThreeCommand<Cardholder>
    {
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
            AllObjects = FilteredCollection = new ObservableCollection<Cardholder>(RandomDataGenerator.Cardholders(10));
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
