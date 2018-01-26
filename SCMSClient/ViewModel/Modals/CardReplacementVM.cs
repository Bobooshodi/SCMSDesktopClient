using SCMSClient.Models;

namespace SCMSClient.ViewModel
{
    public class CardReplacementVM : BaseModalsVM<SOAReplaceCardRequest>
    {
        public CardReplacementVM(SOAReplaceCardRequest _selectedRequest) : base(_selectedItem: _selectedRequest)
        {

        }

        protected override void Process()
        {
            throw new System.NotImplementedException();
        }
    }
}
