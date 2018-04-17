using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using System.Collections.Generic;

namespace SCMSClient.Services.Implementation
{
    public class VehicleService : AbstractService<Vehicle>, IVehicleService
    {
        public VehicleService(IHTTPService _service) : base(_httpService: _service)
        {
        }
    }
}