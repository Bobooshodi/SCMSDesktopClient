using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SCMSClient.ViewModel
{
    public class CardholderDetailsVM : BaseModalsVM<Cardholder>
    {
        private IEmployeeService empService;
        private ITenantService tenantService;

        public CardholderDetailsVM(ICardholderService service, IEmployeeService _empService,
            ITenantService _tenantService) : base(_service: service)
        {
            empService = _empService;
            tenantService = _tenantService;

            if (SelectedItem?.UserType == SHCCardType.Employee)
                SelectedItem = empService.Get(SelectedItem.ID);
            if (SelectedItem?.UserType == SHCCardType.Tenant)
                SelectedItem = tenantService.Get(SelectedItem.ID);
        }

        public Brush StatusColor
        {
            get
            {
                switch (SelectedItem?.Status)
                {
                    case CardUserStatus.Active:
                        return Brushes.Green;
                    case CardUserStatus.Blacklist:
                        return Brushes.Red;
                    case CardUserStatus.Inactive:
                        return Brushes.Orange;
                    default:
                        return Brushes.Red;
                }
            }
        }

        public bool ShowDepartment
        {
            get => SelectedItem?.UserType == SHCCardType.Employee;
        }

        public Card SelectedCard { get; set; }

        public Vehicle SelectedVehicle { get; set; }

        public CarPark SelectedParking { get; set; }

        public bool RowDetailsVisibility { get; set; }


        protected override Task ProcessLogic()
        {
            return null;
        }

        protected override void CloseModal()
        {
            SelectedParking.IsExpanded = !SelectedParking.IsExpanded;
        }
    }
}
