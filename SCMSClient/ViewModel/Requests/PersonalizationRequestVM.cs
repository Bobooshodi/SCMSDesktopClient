using SCMSClient.Models;
using SCMSClient.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace SCMSClient.ViewModel
{
    public class PersonalizationRequestVM : BaseRequestVM<SOAPersonalizationRequest>
    {
        #region Private Methods

        protected override void LoadAllRequests()
        {
            AllRequests = new ObservableCollection<SOAPersonalizationRequest>(RandomDataGenerator.PersonalizationRequests(10));
        }

        protected override bool RequestSearchFilter(object obj)
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

        protected override void ProcessRequest()
        {
            var modal = new Modals.PersonaliseCard(SelectedRequest);

            MessengerInstance.Send<UIElement>(modal);
        }

        #endregion
    }
}
