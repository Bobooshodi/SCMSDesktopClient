using SCMSClient.Models;
using SCMSClient.Services.Interfaces;

namespace SCMSClient.Services.Implementation
{
    public class BuildingService : AbstractService<Building>, IBuildingService
    {
        public BuildingService(IHTTPService _service) : base(_httpService: _service)
        {
        }
    }
}