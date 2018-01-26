using SCMSClient.Modals;
using SCMSClient.Models;
using SCMSClient.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace SCMSClient.ViewModel
{
    public class ReplaceCardRequestVM : BaseRequestVM<SOAReplaceCardRequest>
    {
        #region Private Methods

        protected override void LoadAllRequests()
        {
            AllRequests = new ObservableCollection<SOAReplaceCardRequest>(RandomDataGenerator.ReplaceCardRequests(10));
        }

        protected override bool RequestSearchFilter(object obj)
        {
            var request = obj as SOAReplaceCardRequest;

            return request?.Cardholder?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.CardId?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.ReplacedBy?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || request?.ReplacedCardId?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        #endregion


        #region Command Methods

        protected override void ProcessRequest()
        {
            var modal = new ReplaceCard(SelectedRequest);

            MessengerInstance.Send<UIElement>(modal);
        }

        #endregion
    }
}
