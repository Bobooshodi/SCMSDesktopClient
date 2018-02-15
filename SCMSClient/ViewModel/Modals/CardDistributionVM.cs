using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using System.Threading.Tasks;

namespace SCMSClient.ViewModel
{
    public class CardDistributionVM : BaseModalsVM<SOACardRequest>
    {
        #region Member Declarations
        private int intValue;
        private string distributeFrom;
        private string distributeTo;

        public CardDistributionVM(ICardRequestService service) : base(_service: service)
        {

        }

        /// <summary>
        /// This is the Logic to Enable or Disable the Process Button
        /// </summary>
        protected override bool CanProcess
        {
            get
            {
                if (string.IsNullOrEmpty(DistributeFrom) || string.IsNullOrEmpty(DistributeTo) || IsProcessing ||
                    !int.TryParse(DistributeFrom, out intValue) || !int.TryParse(DistributeTo, out intValue))
                {
                    return false;
                }

                return true;
            }
        }

        #endregion


        #region Public Properties

        /// <summary>
        /// This holds the value of the From Textbox in the View
        /// </summary>
        public string DistributeFrom
        {
            get => distributeFrom;
            set => Set(ref distributeFrom, value, true);
        }

        /// <summary>
        /// This holds the value of the To Textbox in the View
        /// </summary>
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
            await Task.Delay(15000);

            throw new System.Exception("Test");
        }

        #endregion
    }
}
