using SCMSClient.Models;

namespace SCMSClient.ViewModel
{
    public class CardPersonalizationVM : BaseModalsVM<SOAPersonalizationRequest>
    {
        public CardPersonalizationVM(SOAPersonalizationRequest _selectedRequest) : base(_selectedItem: _selectedRequest)
        {
        }

        protected override void Process()
        {
            throw new System.NotImplementedException();
        }
    }
}
