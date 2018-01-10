using System;

namespace SCMSClient.Models
{
    public class Card : BaseModel
    {
        private string mifareId, accessControlNumber;
        private CIN cin;
        private DateTime dateCreated;
        private CardType cardType;
        private CardVendor vendor;
        private BusinessUnit businessUnit;
        private Status status;

        public string MifareId
        {
            get => mifareId;
            set
            {
                Set(ref mifareId, value);
                RaisePropertyChanged(() => MifareId);
            }
        }

        public CIN CIN
        {
            get => cin;
            set
            {
                Set(ref cin, value);
                RaisePropertyChanged(() => CIN);
            }
        }

        public string AccessControlNumber
        {
            get => accessControlNumber;
            set
            {
                Set(ref accessControlNumber, value);
                RaisePropertyChanged(() => AccessControlNumber);
            }
        }

        public DateTime DateRegistered
        {
            get => dateCreated;
            set
            {
                Set(ref dateCreated, value);
                RaisePropertyChanged(() => DateRegistered);
            }
        }

        public CardType CardType
        {
            get => cardType;
            set
            {
                Set(ref cardType, value);
                RaisePropertyChanged(() => CardType);
            }
        }

        public CardVendor Vendor
        {
            get => vendor;
            set
            {
                Set(ref vendor, value);
                RaisePropertyChanged(() => Vendor);
            }
        }

        public BusinessUnit BusinessUnit
        {
            get => businessUnit;
            set
            {
                Set(ref businessUnit, value);
                RaisePropertyChanged(() => BusinessUnit);
            }
        }

        public Status Status
        {
            get => status;
            set
            {
                Set(ref status, value);
                RaisePropertyChanged(() => Status);
            }
        }
    }

    public class CIN
    {
        public string InverntoryNumber { get; set; }
        public DateTime DateGenerated { get; set; }
    }
}
