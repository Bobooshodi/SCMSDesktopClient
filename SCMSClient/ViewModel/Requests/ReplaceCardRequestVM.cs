using SCMSClient.Modals;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using System;
using System.Windows;

namespace SCMSClient.ViewModel
{
    /// <summary>
    /// ViewModel Logic for the Card Replacement Page
    /// </summary>
    public class ReplaceCardRequestVM : CollectionsVMWithOneCommand<SOAReplaceCardRequest>
    {
        public override bool IsBusy { get; set; }

        #region Default Constructor

        /// <summary>
        /// This Class' Implementation of the Base class' constructor
        /// </summary>
        /// <param name="_service">
        /// The default Service class that manages objects of the type 
        /// inferred from the Argument passed to the base class
        /// </param>
        public ReplaceCardRequestVM(ICardReplacementService _service) : base(_service: _service)
        {
        }

        #endregion


        #region Private Methods

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
