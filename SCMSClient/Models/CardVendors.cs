using System.ComponentModel.DataAnnotations;

namespace SCMSClient.Models
{
    public class CardVendor : BaseModel
    {
        private string name, description, address, emailAddress;
        private long contactNumber;

        [Required(ErrorMessage = "Please, Enter a Name for this Vendor")]
        public string Name
        {
            get { return name; }
            set
            {
                Set(ref name, value);
                RaisePropertyChanged(() => Name);
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                Set(ref description, value);
                RaisePropertyChanged(() => Description);
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                Set(ref address, value);
                RaisePropertyChanged(() => Address);
            }
        }

        [Required(ErrorMessage = "Please, Enter the email of this Vendor")]
        [EmailAddress(ErrorMessage = "Please, Enter a Valid Email")]
        public string EmailAddress
        {
            get { return emailAddress; }
            set
            {
                Set(ref address, value);
                RaisePropertyChanged(() => EmailAddress);
            }
        }

        [Required(ErrorMessage = "Please, Enter the Phone Number of this Vendor")]
        [Phone(ErrorMessage = "Please, Enter a Valid Phone Number")]
        public long ContactNumber
        {
            get { return contactNumber; }
            set
            {
                Set(ref contactNumber, value);
                RaisePropertyChanged(() => ContactNumber);
            }
        }

    }
}
