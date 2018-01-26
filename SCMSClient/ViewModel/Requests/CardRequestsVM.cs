using SCMSClient.Modals;
using SCMSClient.Models;
using SCMSClient.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace SCMSClient.ViewModel
{
    public class CardRequestsVM : BaseRequestVM<SOACardRequest>
    {
        #region Private Methods

        protected override void LoadAllRequests()
        {
            AllRequests = new ObservableCollection<SOACardRequest>(RandomDataGenerator.CardRequests(10));
        }

        protected override bool RequestSearchFilter(object obj)
        {
            var request = obj as SOACardRequest;

            return request?.RequestedBy?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.CardType.Name?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.RequestId?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.BusinessUnit?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        #endregion


        #region Command Methods

        protected override void ProcessRequest()
        {
            var modal = new DistributeCards(SelectedRequest);

            MessengerInstance.Send<UIElement>(modal);
        }

        #endregion
    }
}
