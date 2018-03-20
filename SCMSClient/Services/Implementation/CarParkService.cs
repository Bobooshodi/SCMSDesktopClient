using SCMSClient.Models;
using SCMSClient.Services.Interfaces;

namespace SCMSClient.Services.Implementation
{
    public class CarParkService : AbstractService<CarPark>, ICarParkService
    {
        public CarParkService(IHTTPService _service) : base(_httpService: _service)
        {

        }
    }
}
