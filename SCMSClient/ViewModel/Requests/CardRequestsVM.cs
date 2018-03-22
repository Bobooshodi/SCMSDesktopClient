using SCMSClient.Modals;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using System;
using System.Windows;

namespace SCMSClient.ViewModel
{
    /// <summary>
    /// ViewModel Logic for the CardRequest's Page
    /// </summary>
    public class CardRequestsVM : CollectionsVMWithOneCommand<SOACardRequest>
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
        public CardRequestsVM(ICardRequestService _service, IDinkeyDongleService _dongleService) : base(_service: _service, _dongleService: _dongleService)
        {
        }

        #endregion Default Constructor

        #region Inherited Methods

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
            var request = obj as SOACardRequest;

            return request?.RequestedBy?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.CardType.Name?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.RequestId?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.BusinessUnit?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        #endregion Inherited Methods

        #region Command Methods

        /// <summary>
        /// Implementation of the Logic to process the
        /// Selected <see cref="SOACardRequest"/> Object
        /// </summary>
        protected override void Process()
        {
            var modal = new DistributeCards(SelectedObject);

            MessengerInstance.Send<UIElement>(modal);
        }

        #endregion Command Methods
    }
}