using SCMSClient.Models;
using System.Threading.Tasks;

namespace SCMSClient.ViewModel
{
    public class CardDistributionVM : BaseModalsVM<SOACardRequest>
    {
        #region Member Declarations
        private string distributeFrom;
        private string distributeTo;
        protected override bool CanProcess
        {
            get
            {
                if (string.IsNullOrEmpty(DistributeFrom) || string.IsNullOrEmpty(DistributeTo) || IsProcessing)
                    return false;

                return true;
            }
        }

        #endregion


        #region Default Constructor

        /// <summary>
        /// This Class' implementation of the Base Class' constructor
        /// </summary>
        public CardDistributionVM()
        {

        }

        #endregion


        #region Public Properties

        public string DistributeFrom
        {
            get => distributeFrom;
            set => Set(ref distributeFrom, value, true);
        }

        public string DistributeTo
        {
            get => distributeTo;
            set => Set(ref distributeTo, value, true);
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
