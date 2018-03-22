using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using System;
using System.Threading.Tasks;

namespace SCMSClient.ViewModel
{
    /// <summary>
    /// The ViewNodel Logic for the Card Personalization Modal
    /// </summary>
    public class CardPersonalizationVM : BaseModalsVM<SOAPersonalizationRequest>
    {
        public CardPersonalizationVM(IPersonalizationRequestService service, IDinkeyDongleService _dongleService) : base(_service: service, _dongleService: _dongleService)
        {
        }

        public string PageHeader { get; set; }

        #region Inherited Methods

        /// <summary>
        /// This Class' Implementation of the Process Logic defined in the Base Class
        /// </summary>
        protected override async Task ProcessLogic()
        {
            try
            {
                await Task.Delay(15000);
                throw new Exception("Test");
            }
            catch (Exception ex)
            {
                toastManager.ShowErrorToast(Toaster.ErrorTitle, ex.Message);
            }
        }

        #endregion Inherited Methods
    }
}