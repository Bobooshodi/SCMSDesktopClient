using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using System.Threading.Tasks;

namespace SCMSClient.ViewModel
{
    public class CardReplacementVM : BaseModalsVM<SOAReplaceCardRequest>
    {
        public CardReplacementVM(ICardReplacementService service, IDinkeyDongleService _dongleService) : base(_service: service, _dongleService: _dongleService)
        {
        }

        #region Inherited Methods

        /// <summary>
        /// This Class' Implementation of the Process Logic defined in the Base Class
        /// </summary>
        protected override async Task ProcessLogic()
        {
            await Task.Delay(15000);

            toastManager.ShowSuccessToast(Toaster.SuccessTitle, "Succss!!!");
        }

        #endregion Inherited Methods
    }
}