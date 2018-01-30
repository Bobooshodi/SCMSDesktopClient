using SCMSClient.Models;

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
        /// <param name="_selectedRequest">
        /// The Item Selected from the List
        /// </param>
        public CardPersonalizationVM(SOAPersonalizationRequest _selectedRequest) : base(_selectedItem: _selectedRequest)
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
