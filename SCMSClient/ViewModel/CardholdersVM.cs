using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class CardholdersVM : CollectionsVMWithTwoCommands<Cardholder>
    {
        private readonly IEmployeeService empService;
        private readonly ITenantService tenantService;

        #region Default Constructor

        public CardholdersVM(ICardholderService _cardholderService, IEmployeeService _empService,
            ITenantService _tenantService) : base(_service: _cardholderService)
        {
            empService = _empService;
            tenantService = _tenantService;

            AddCardholderCommand = new RelayCommand(OpenCardholderRegistrationPage);
        }


        #endregion


        public ICommand AddCardholderCommand { get; set; }


        #region Private Methods

        protected override bool SearchFilter(object obj)
        {
            var cardholder = obj as Cardholder;

            if (cardholder?.FullName?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0
                || cardholder?.IdentificationNo?.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            return false;
        }

        #endregion


        #region Command Methods

        protected override void Process()
        {
            var detailsVm = SimpleIoc.Default.GetInstance<CardholderDetailsVM>();
            detailsVm.SelectedItem = SelectedObject;

            var mainWindowVm = SimpleIoc.Default.GetInstance<MainWindowVM>();
            mainWindowVm.ActivePage = new Uri("/Views/CardholderDetails.xaml", UriKind.RelativeOrAbsolute);
        }

        private void OpenCardholderRegistrationPage()
        {
            var regVM = SimpleIoc.Default.GetInstance<CardholderRegistrationVM>();
            regVM.CreateSeparateVM = true;

            var mainWindowVm = SimpleIoc.Default.GetInstance<MainWindowVM>();
            mainWindowVm.ActivePage = new Uri("/Views/RegisterCardholder.xaml", UriKind.RelativeOrAbsolute);
        }

        protected override void FilterCollections(object obj)
        {
            var filter = obj as string;

            var cardholders = AllObjects
                .Where(ch => ch.Cards
                                .Any(c => c.CardType.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0))
                                .ToList();

            FilteredCollection = new ObservableCollection<Cardholder>(cardholders);
        }

        #endregion
    }
}
