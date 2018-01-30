using SCMSClient.Models;

namespace SCMSClient.ViewModel
{
    public class CardDistributionVM : BaseModalsVM<SOACardRequest>
    {
        #region Default Constructor

        /// <summary>
        /// This Class' implementation of the Base Class' constructor
        /// </summary>
        /// <param name="_SelectedRequest">
        /// The Item Selected from the List
        /// </param>
        public CardDistributionVM(SOACardRequest _SelectedRequest) : base(_selectedItem: _SelectedRequest)
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
