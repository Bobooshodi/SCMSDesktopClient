using System.Threading.Tasks;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;

namespace SCMSClient.ViewModel
{
    public class AddVehicleVM : BaseModalsVM<Vehicle>
    {

        public AddVehicleVM(IVehicleService service) : base(_service: service)
        {

        }

        public string PlateNumber { get; set; }

        public string CarModel { get; set; }

        public string PlateBorderStyle
        {
            get
            {
                return DisplayError(() => PlateErrorTextVisibility);
            }
        }

        public string ModelBorderStyle
        {
            get
            {
                return DisplayError(() => ModelErrorTextVisibility);
            }
        }

        public bool PlateErrorTextVisibility
        {
            get => PlateNumber?.Length == 0;
        }

        public bool ModelErrorTextVisibility
        {
            get => CarModel?.Length == 0;
        }



        protected override Task ProcessLogic()
        {
            throw new System.NotImplementedException();
        }
    }
}
