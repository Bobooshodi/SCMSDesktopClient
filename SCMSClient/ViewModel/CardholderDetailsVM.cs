using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SCMSClient.ViewModel
{
    public class CardholderDetailsVM : BaseModalsVM<Cardholder>
    {
        private IEmployeeService empService;
        private ITenantService tenantService;
        private ICardTypeService cardTypeService;

        public CardholderDetailsVM(ICardholderService service, IEmployeeService _empService,
            ITenantService _tenantService, ICardTypeService _cardTypeService) : base(_service: service)
        {
            empService = _empService;
            tenantService = _tenantService;
            cardTypeService = _cardTypeService;

            if (SelectedItem?.UserType == SHCCardType.Employee)
                SelectedItem = empService.Get(SelectedItem.ID);
            if (SelectedItem?.UserType == SHCCardType.Tenant)
                SelectedItem = tenantService.Get(SelectedItem.ID);

            SupplementaryCommand = new RelayCommand(ProcessSupplementary);
        }

        public ICommand SupplementaryCommand { get; set; }

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

        private void ProcessSupplementary()
        {
            //var cardType = cardTypeService.Get(SelectedCard.CardTypeId);

            var request = new SOAPersonalizationRequest
            {
                Cardholder = SelectedItem.FullName,
                CardholderId = SelectedItem.ID,
                IdentificationNo = SelectedItem.IdentificationNo,
                RequestDate = DateTime.Now,
                CardInventoryNo = SelectedCard.CardInventoryNo,
                IdentificationType = SelectedItem.IdentificationType,
                PersonalizationStatus = RequestStatus.New,
                CardType = new CardType
                {
                    Name = SelectedCard.CardType
                }
            };

            var modal = new Modals.PersonaliseCard(request);
            MessengerInstance.Send<UIElement>(modal);
        }
    }
}
