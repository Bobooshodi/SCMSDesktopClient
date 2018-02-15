using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using System.Threading.Tasks;

namespace SCMSClient.ViewModel
{
    /// <summary>
    /// The ViewNodel Logic for the Card Personalization Modal
    /// </summary>
    public class CardPersonalizationVM : BaseModalsVM<SOAPersonalizationRequest>
    {
        public CardPersonalizationVM(IPersonalizationRequestService service) : base(_service: service)
        {

        }

        #region Inherited Methods

        /// <summary>
        /// This Class' Implementation of the Process Logic defined in the Base Class
        /// </summary>
        protected override async Task ProcessLogic()
        {
            await Task.Delay(15000);

            throw new System.Exception("Test");
        }

        #endregion
    }
}
