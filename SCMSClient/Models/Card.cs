using System;

namespace SCMSClient.Models
{
    public class Card : BaseModel
    {
        private string mifareId, cin, accessControlNumber;
        private DateTime dateCreated;
        private CardType cardType;
        private CardVendor vendor;
        private BusinessUnit businessUnit;
        private Status status;

        public string MifareId
        {
            get { return mifareId; }
            set
            {
                Set(ref mifareId, value);
                RaisePropertyChanged(() => MifareId);
            }
        }

        public string CIN
        {
            get { return cin; }
            set
            {
                Set(ref cin, value);
                RaisePropertyChanged(() => CIN);
            }
        }

        public string AccessControlNumber
        {
            get { return accessControlNumber; }
            set
            {
                Set(ref accessControlNumber, value);
                RaisePropertyChanged(() => AccessControlNumber);
            }
        }

        public DateTime DateCreated
        {
            get { return dateCreated; }
            set
            {
                Set(ref dateCreated, value);
                RaisePropertyChanged(() => DateCreated);
            }
        }

        public CardType CardType
        {
            get { return cardType; }
            set
            {
                Set(ref cardType, value);
                RaisePropertyChanged(() => CardType);
            }
        }

        public CardVendor Vendor
        {
            get { return vendor; }
            set
            {
                Set(ref vendor, value);
                RaisePropertyChanged(() => Vendor);
            }
        }

        public BusinessUnit BusinessUnit
        {
            get { return businessUnit; }
            set
            {
                Set(ref businessUnit, value);
                RaisePropertyChanged(() => BusinessUnit);
            }
        }

        public Status Status
        {
            get { return status; }
            set
            {
                Set(ref status, value);
                RaisePropertyChanged(() => Status);
            }
        }
    }
}
