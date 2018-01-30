using SCMSClient.Modals;
using SCMSClient.Models;
using SCMSClient.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace SCMSClient.ViewModel
{
    /// <summary>
    /// ViewModel Logic for the Card Replacement Page
    /// </summary>
    public class ReplaceCardRequestVM : CollectionsVMWithOneCommand<SOAReplaceCardRequest>
    {
        #region Private Methods

        /// <summary>
        /// Implementation of the Logic to Load all <see cref="SOAReplaceCardRequest"/>
        /// </summary>
        protected override void LoadAll()
        {
            AllObjects = new ObservableCollection<SOAReplaceCardRequest>(RandomDataGenerator.ReplaceCardRequests(10));
        }

        /// <summary>
        /// Implementaton of the Logic to filter the Collection
        /// </summary>
        /// <param name="obj">
        /// represents an object in the collection
        /// </param>
        /// <returns>
        /// returns true if the filter condition passes or
        /// false if the filter condition fails
        /// </returns>
        protected override bool SearchFilter(object obj)
        {
            var request = obj as SOAReplaceCardRequest;

            return request?.Cardholder?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.CardId?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.ReplacedBy?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.ReplacedCardId?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        #endregion


        #region Command Methods

        /// <summary>
        /// Implementation of the Logic to process the
        /// Selected <see cref="SOAReplaceCardRequest"/> Object
        /// </summary>
        protected override void Process()
        {
            var modal = new ReplaceCard(SelectedObject);

            MessengerInstance.Send<UIElement>(modal);
        }

        #endregion
    }
}
