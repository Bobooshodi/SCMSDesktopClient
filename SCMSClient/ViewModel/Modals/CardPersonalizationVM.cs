using SCMSClient.Models;
using System.Threading.Tasks;

namespace SCMSClient.ViewModel
{
    /// <summary>
    /// The ViewNodel Logic for the Card Personalization Modal
    /// </summary>
    public class CardPersonalizationVM : BaseModalsVM<SOAPersonalizationRequest>
    {
        #region Default Constructor

        /// <summary>
        /// This Class' implementation of the Base Class' constructor
        /// </summary>
        public CardPersonalizationVM()
        {
        }

        #endregion


        #region Inherited Methods

        /// <summary>
        /// This Class' Implementation of the Process Logic defined in the Base Class
        /// </summary>
        protected override async Task ProcessLogic()
        {
        }

        #endregion
    }
}
