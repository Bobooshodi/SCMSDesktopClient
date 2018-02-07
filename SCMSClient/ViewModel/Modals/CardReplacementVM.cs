using SCMSClient.Models;

namespace SCMSClient.ViewModel
{
    public class CardReplacementVM : BaseModalsVM<SOAReplaceCardRequest>
    {
        #region Default Constructor

        /// <summary>
        /// This Class' implementation of the Base Class' constructor
        /// </summary>
        /// <param name="_selectedRequest">
        /// The Item Selected from the List
        /// </param>
        public CardReplacementVM()
        {

        }

        #endregion


        #region Inherited Methods

        /// <summary>
        /// This Class' Implementation of the Process Logic defined in the Base Class
        /// </summary>
        protected override void Process()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
