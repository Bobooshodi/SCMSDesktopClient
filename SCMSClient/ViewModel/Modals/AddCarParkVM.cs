using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SCMSClient.ViewModel
{
    public class AddCarParkVM : BaseModalsVM<CarPark>
    {
        public AddCarParkVM(ICarParkService service, IDinkeyDongleService _dongleService) : base(_service: service, _dongleService: _dongleService)
        {
        }

        protected override Task ProcessLogic()
        {
            throw new NotImplementedException();
        }
    }
}