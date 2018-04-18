using GalaSoft.MvvmLight;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SCMSClient.ViewModel
{
    public class DashboardVM : ViewModelBase
    {
        private Toaster toaster = Toaster.Instance;

        private ICardService cardService;
        private ICardRequestService sCardRequests;
        private ICardholderService cardholderService;
        private ICardReplacementService cardReplacements;
        private IPersonalizationRequestService sPersonalization;

        public DashboardVM(ICardService _cardService, ICardRequestService _cardRequests,
        ICardholderService _cardholderService, ICardReplacementService _cardReplacements,
        IPersonalizationRequestService _personalizationRequests)
        {
            cardService = _cardService;
            sCardRequests = _cardRequests;
            cardReplacements = _cardReplacements;
            cardholderService = _cardholderService;
            sPersonalization = _personalizationRequests;

            LoadAll().ConfigureAwait(false);
        }

        private async Task LoadAll()
        {
            try
            {
                IsCardBusy = true;
                IsRequestsBusy = true;
                IsCardholdersBusy = true;

                await Task.Run(() =>
                {
                    Cards = cardService.GetAll();
                    CardRequests = sCardRequests.GetAll();
                    Cardholders = cardholderService.GetAll();
                    ReplacementRequests = cardReplacements.GetAll();
                    PersonalizationRequests = sPersonalization.GetAll();

                    ProcessCards();
                    ProcessCardholders();

                    IsRequestsBusy = false;
                });
            }
            catch (Exception ex)
            {
                toaster.ShowErrorToast(Toaster.ErrorTitle, ex.Message);
            }
        }

        public List<Card> Cards { get; set; }
        public List<Cardholder> Cardholders { get; set; }
        public List<SOACardRequest> CardRequests { get; set; }
        public List<SOAReplaceCardRequest> ReplacementRequests { get; set; }
        public List<SOAPersonalizationRequest> PersonalizationRequests { get; set; }

        public bool IsCardBusy { get; set; }
        public bool IsRequestsBusy { get; set; }
        public bool IsCardholdersBusy { get; set; }

        public GridLength StrataCardholdersGridLength { get; set; }
        public GridLength TenantCardholdersGridLength { get; set; }
        public GridLength EmployeeCardholdersGridLength { get; set; }
        public GridLength IndividualCardholdersGridLength { get; set; }

        public double StrataCardholdersPercentage { get; set; }
        public double TenantCardholdersPercentage { get; set; }
        public double EmployeeCardholdersPercentage { get; set; }
        public double IndividualCardholdersPercentage { get; set; }

        public List<Cardholder> StrataCardholders { get; set; }
        public List<Cardholder> TenantCardholders { get; set; }
        public List<Cardholder> EmployeeCardholders { get; set; }
        public List<Cardholder> IndividualCardholders { get; set; }

        public GridLength StrataCardsGridLength { get; set; }
        public GridLength TenantCardsGridLength { get; set; }
        public GridLength PhantomCardsGridLength { get; set; }
        public GridLength EmployeeCardsGridLength { get; set; }
        public GridLength IndividualCardsGridLength { get; set; }

        public double StrataCardsPercentage { get; set; }
        public double TenantCardsPercentage { get; set; }
        public double EmployeeCardsPercentage { get; set; }
        public double IndividualCardsPercentage { get; set; }
        public GridLength PhantomCardsPercentage { get; set; }

        public List<Card> StrataCards { get; set; }
        public List<Card> TenantCards { get; set; }
        public GridLength PhantomCards { get; set; }
        public List<Card> EmployeeCards { get; set; }
        public List<Card> IndividualCards { get; set; }

        /**
        public GridLength EmployeeCardsPercentage
        {
            get
            {
                var employees = Cards.Where(c => c.CardType == SHCCardType.Employee).ToList();

                return new GridLength(CalculatePercentage(employees.Count, cards.Count), GridUnitType.Star);
            }
        }

        public GridLength TenantCardsPercentage
        {
            get
            {
                var tenants = cards.Where(c => c.UserType == SHCCardType.Tenant).ToList();

                return new GridLength(CalculatePercentage(tenants.Count, cards.Count), GridUnitType.Star);
            }
        }

        public GridLength IndividualCardsPercentage
        {
            get
            {
                var individuals = cards.Where(c => c.UserType == SHCCardType.Individual).ToList();

                return new GridLength(CalculatePercentage(individuals.Count, cards.Count), GridUnitType.Star);
            }
        }

        public GridLength StrataCardsPercentage
        {
            get
            {
                var strata = cards.Where(c => c.UserType == SHCCardType.Strata).ToList();

                return new GridLength(CalculatePercentage(strata.Count, cards.Count), GridUnitType.Star);
            }
        }

        */

        private void ProcessCardholders()
        {
            StrataCardholders = Cardholders.Where(c => c.UserType == SHCCardType.Strata).ToList();
            TenantCardholders = Cardholders.Where(c => c.UserType == SHCCardType.Tenant).ToList();
            EmployeeCardholders = Cardholders.Where(c => c.UserType == SHCCardType.Employee).ToList();
            IndividualCardholders = Cardholders.Where(c => c.UserType == SHCCardType.Individual).ToList();

            StrataCardholdersPercentage = CalculatePercentage(StrataCardholders.Count, Cardholders.Count);
            TenantCardholdersPercentage = CalculatePercentage(TenantCardholders.Count, Cardholders.Count);
            EmployeeCardholdersPercentage = CalculatePercentage(EmployeeCardholders.Count, Cardholders.Count);
            IndividualCardholdersPercentage = CalculatePercentage(IndividualCardholders.Count, Cardholders.Count);

            StrataCardholdersGridLength = new GridLength(StrataCardholdersPercentage, GridUnitType.Star);
            TenantCardholdersGridLength = new GridLength(TenantCardholdersPercentage, GridUnitType.Star);
            EmployeeCardholdersGridLength = new GridLength(EmployeeCardholdersPercentage, GridUnitType.Star);
            IndividualCardholdersGridLength = new GridLength(IndividualCardholdersPercentage, GridUnitType.Star);

            IsCardholdersBusy = false;
        }

        private void ProcessCards()
        {
        }

        private double CalculatePercentage(int amount, double total)
        {
            return (amount / total) * 100;
        }
    }
}