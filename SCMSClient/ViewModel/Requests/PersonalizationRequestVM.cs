using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace SCMSClient.ViewModel
{
    /// <summary>
    /// ViewModel Logic for the Personalization requests page
    /// </summary>
    public class PersonalizationRequestVM : CollectionsVMWithOneCommand<SOAPersonalizationRequest>
    {
        private readonly IPersonalizationRequestService service;

        #region Default Constructor

        public PersonalizationRequestVM(IPersonalizationRequestService _service)
        {
            service = _service;

            Load();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Implementation of the Logic to Load all <see cref="SOAPersonalizationRequest"/>
        /// </summary>
        protected async Task Load()
        {
            try
            {
                await Task.Run(() => AllObjects = new ObservableCollection<SOAPersonalizationRequest>(service.GetAll()));
            }
            catch (Exception e)
            {
                toaster.ShowErrorToast(Toaster.ErrorTitle, e.Message);
            }
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
            var request = obj as SOAPersonalizationRequest;

            return request.Cardholder.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request.IdentificationNo.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request.RequestId.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request.ContractNo.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request.CardInventoryNo.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        #endregion


        #region Command Methods

        /// <summary>
        /// Implementation of the Logic to process the
        /// Selected <see cref="SOAPersonalizationRequest"/> Object
        /// </summary>
        protected override void Process()
        {
            var modal = new Modals.PersonaliseCard(SelectedObject);

            MessengerInstance.Send<UIElement>(modal);
        }

        protected override void LoadAll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
