using SCMSClient.Models;

namespace SCMSClient.ViewModel
{
    public class CardDistributionVM : BaseModalsVM<SOACardRequest>
    {
        public CardDistributionVM(SOACardRequest _SelectedRequest) : base(_selectedItem: _SelectedRequest)
        {

        }

        protected override void Process()
        {
            throw new System.NotImplementedException();
        }
    }
}
