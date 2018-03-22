using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using System;
using System.Threading.Tasks;

namespace SCMSClient.ViewModel
{
    public class VerifyCardVM : BaseModalsVM<Card>
    {
        private readonly ICardholderService cardholderService;
        private readonly ICardReaderService cardReader;

        public VerifyCardVM(ICardService _service, ICardholderService _cardholderService, ICardReaderService _cardReader,
            IDinkeyDongleService _dongleService) : base(_service: _service, _dongleService: _dongleService)
        {
            cardReader = _cardReader;
            cardholderService = _cardholderService;
        }

        protected override async Task ProcessLogic()
        {
            try
            {
                await RunMethodAsync(() =>
               {
                   var cardId = cardReader.ReadCardSerialNumber();

                   SelectedItem = service.Get(cardId);

                   Feedback = successFeedback;

                   if (SelectedItem == null)
                   {
                       throw new Exception("There's no Card registered with this Id");
                   }

                   Cardholder = cardholderService.Get(SelectedItem.AssignedToId);

                   if (Cardholder == null)
                   {
                       throw new Exception("There's nobody assigned to this card");
                   }
               });
            }
            catch (Exception ex)
            {
                Feedback = errorFeedback;
                toastManager.ShowErrorToast(Toaster.ErrorTitle, ex.Message);
            }
        }

        public Cardholder Cardholder { get; set; }
    }
}