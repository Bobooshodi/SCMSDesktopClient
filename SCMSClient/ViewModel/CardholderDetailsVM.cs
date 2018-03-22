using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Modals;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace SCMSClient.ViewModel
{
    public class CardholderDetailsVM : BaseModalsVM<Cardholder>
    {
        private IEmployeeService empService;
        private ITenantService tenantService;
        private ICardTypeService cardTypeService;
        private ActiveTab activeTab;
        private List<Card> allCards;
        private List<Vehicle> allVehicles;
        private List<CarPark> allParking;
        private List<Building> allBuildings;
        private string cardsFilterText, vehiclesFilterText, parkingFilterText, buildingsFilterText;

        public CardholderDetailsVM(ICardholderService service, IEmployeeService _empService,
            ITenantService _tenantService, ICardTypeService _cardTypeService, IDinkeyDongleService _dongleService) : base(_service: service, _dongleService: _dongleService)
        {
            empService = _empService;
            tenantService = _tenantService;
            cardTypeService = _cardTypeService;
            AddCardVisibility = true;

            AddCardCommand = new RelayCommand(OpenAddCardModal);
            AddVehicleCommand = new RelayCommand(OpenAddVehicleModal);
            AddParkingCommand = new RelayCommand(OpenAddParkingModal);
            AddBuildingCommand = new RelayCommand(OpenAddBuildingModal);
            SupplementaryCommand = new RelayCommand(ProcessSupplementary);

            LoadCardholderInfo().ConfigureAwait(false);
        }

        private async Task LoadCardholderInfo()
        {
            if (SelectedItem?.UserType == SHCCardType.Employee)
            {
                using (empService)
                {
                    SelectedItem = await RunMethodAsync(() => empService.Get(SelectedItem.ID), () => IsProcessing);
                }
            }
            if (SelectedItem?.UserType == SHCCardType.Tenant)
            {
                using (tenantService)
                {
                    SelectedItem = await RunMethodAsync(() => tenantService.Get(SelectedItem.ID), () => IsProcessing);
                }
            }
        }

        public override Cardholder SelectedItem
        {
            get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;

                LoadCardholderInfo().ConfigureAwait(false);
            }
        }

        public ICommand SupplementaryCommand { get; set; }
        public ICommand AddCardCommand { get; set; }
        public ICommand AddVehicleCommand { get; set; }
        public ICommand AddParkingCommand { get; set; }
        public ICommand AddBuildingCommand { get; set; }

        public ICollectionView CardsCollection { get; set; }
        public ICollectionView VehiclesCollection { get; set; }
        public ICollectionView ParkingCollection { get; set; }
        public ICollectionView BuildingsCollection { get; set; }

        public ActiveTab ActiveTab
        {
            get => activeTab;
            set
            {
                Set(ref activeTab, value, true);

                ShowOption(value);
            }
        }

        public string CardsFilterText
        {
            get => cardsFilterText ?? (cardsFilterText = string.Empty);
            set
            {
                Set(ref cardsFilterText, value, true);

                CardsCollection.Refresh();
            }
        }

        public string VehiclesFilterText
        {
            get => vehiclesFilterText ?? (vehiclesFilterText = string.Empty);
            set
            {
                Set(ref vehiclesFilterText, value, true);

                VehiclesCollection.Refresh();
            }
        }

        public string ParkingFilterText
        {
            get => parkingFilterText ?? (parkingFilterText = string.Empty);
            set
            {
                Set(ref parkingFilterText, value, true);

                ParkingCollection.Refresh();
            }
        }

        public string BuildingsFilterText
        {
            get => buildingsFilterText ?? (buildingsFilterText = string.Empty);
            set
            {
                Set(ref buildingsFilterText, value, true);

                BuildingsCollection.Refresh();
            }
        }

        public bool AddCardVisibility { get; set; }

        public bool AddVehicleVisibility { get; set; }

        public bool AddParkingVisibility { get; set; }

        public bool AddBuildingVisibility { get; set; }

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

        public List<Card> Cards
        {
            get
            {
                if (SelectedItem.Cards != null && allCards == null)
                {
                    allCards = SelectedItem.Cards;

                    CardsCollection = CollectionViewSource.GetDefaultView(allCards);
                    CardsCollection.Filter = CardsFilter;
                }

                return allCards ?? new List<Card>();
            }
        }

        public List<Vehicle> Vehicles
        {
            get
            {
                if (SelectedItem.Vehicles != null && allVehicles == null)
                {
                    allVehicles = SelectedItem.Vehicles;

                    VehiclesCollection = CollectionViewSource.GetDefaultView(allVehicles);
                    VehiclesCollection.Filter = VehiclesFilter;
                }

                return allVehicles ?? new List<Vehicle>();
            }
        }

        public List<CarPark> Parking
        {
            get
            {
                if (SelectedItem.CarParks != null && allParking == null)
                {
                    allParking = SelectedItem.CarParks;

                    ParkingCollection = CollectionViewSource.GetDefaultView(allParking);
                    ParkingCollection.Filter = ParkingFilter;
                }

                return allParking ?? new List<CarPark>();
            }
        }

        public List<Building> Buildings
        {
            get
            {
                if (SelectedItem.Buildings != null && allBuildings == null)
                {
                    allBuildings = SelectedItem.Buildings;

                    BuildingsCollection = CollectionViewSource.GetDefaultView(allBuildings);
                    BuildingsCollection.Filter = BuildingsFilter;
                }

                return allBuildings ?? new List<Building>();
            }
        }

        private bool CardsFilter(object obj)
        {
            var card = obj as Card;

            return card?.CardInventoryNo?.IndexOf(CardsFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || card?.CardaxNo?.IndexOf(CardsFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || card?.MifareId?.IndexOf(CardsFilterText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private bool VehiclesFilter(object obj)
        {
            var vehicle = obj as Vehicle;

            return vehicle?.NumberPlate?.IndexOf(VehiclesFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || vehicle?.VehicleModel?.IndexOf(VehiclesFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || vehicle?.Brand?.IndexOf(VehiclesFilterText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private bool ParkingFilter(object obj)
        {
            var parking = obj as CarPark;

            return parking?.Building?.IndexOf(ParkingFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || parking?.BayNo?.IndexOf(ParkingFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || parking?.BayLocation?.IndexOf(ParkingFilterText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private bool BuildingsFilter(object obj)
        {
            var building = obj as Building;

            return building?.ContactPerson?.IndexOf(BuildingsFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || building?.Name?.IndexOf(BuildingsFilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || building?.Address?.IndexOf(BuildingsFilterText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public Vehicle SelectedVehicle { get; set; }

        public CarPark SelectedParking { get; set; }

        public bool RowDetailsVisibility { get; set; }

        private void ShowOption(ActiveTab value)
        {
            switch (value)
            {
                case ActiveTab.CARDS:
                    AddCardVisibility = true;
                    AddVehicleVisibility = false;
                    AddParkingVisibility = false;
                    AddBuildingVisibility = false;
                    break;

                case ActiveTab.VEHICLES:
                    AddCardVisibility = false;
                    AddVehicleVisibility = true;
                    AddParkingVisibility = false;
                    AddBuildingVisibility = false;
                    break;

                case ActiveTab.PARKING:
                    AddCardVisibility = false;
                    AddVehicleVisibility = false;
                    AddParkingVisibility = true;
                    AddBuildingVisibility = false;
                    break;

                case ActiveTab.BUILDING:
                    AddCardVisibility = false;
                    AddVehicleVisibility = false;
                    AddParkingVisibility = false;
                    AddBuildingVisibility = true;
                    break;
            }
        }

        protected void OpenModal(UIElement modal)
        {
            MessengerInstance.Send<UIElement>(modal);
        }

        protected override Task ProcessLogic()
        {
            return null;
        }

        protected override void Close()
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
                    ID = SelectedCard.CardTypeId,
                    Name = SelectedCard.CardType
                }
            };

            var modal = new PersonaliseCard(request, true);
            MessengerInstance.Send<UIElement>(modal);
        }

        private void OpenAddBuildingModal()
        {
            var modal = new AddBuilding();
            OpenModal(modal);
        }

        private void OpenAddParkingModal()
        {
            var modal = new AddParking();
            OpenModal(modal);
        }

        private void OpenAddVehicleModal()
        {
            var modal = new AddVehicle();
            OpenModal(modal);
        }

        private void OpenAddCardModal()
        {
            var request = new SOAPersonalizationRequest
            {
                Cardholder = SelectedItem.FullName,
                ID = Guid.NewGuid().ToString(),
                CardholderId = SelectedItem.ID,
            };
            var modal = new PersonaliseCard(request);
            OpenModal(modal);
        }
    }

    public enum ActiveTab
    {
        CARDS,
        VEHICLES,
        PARKING,
        BUILDING
    }
}