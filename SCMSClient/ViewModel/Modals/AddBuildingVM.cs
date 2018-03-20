using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SCMSClient.ViewModel
{
    public class AddBuildingVM : BaseModalsVM<Building>
    {
        public AddBuildingVM(IBuildingService service) : base(_service:service)
        {

        }

        protected override Task ProcessLogic()
        {
            throw new NotImplementedException();
        }
    }
}
