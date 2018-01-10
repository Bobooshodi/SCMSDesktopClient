using System;
using System.ComponentModel.DataAnnotations;

namespace SCMSClient.Models
{
    public class Cardholder : BaseModel
    {
        #region private members

        private string firstName, lastName, preferredName, address,
                        primaryEmail, secondaryEmail, idNo;

        private DateTime dateOfBirth;
        private long handPhoneNumber, workPhoneNumber;
        private CardholderGender gender;
        private IdentificationType iDType;
        private City city;
        private State state;
        private Tenant tenantDetails;
        private Parking parkingDetails;
        private Status status;

        #endregion private members

        #region Public Properties

        [Required(ErrorMessage = "Please, Enter a the First Name of this Cardholder")]
        public string FirstName
        {
            get { return firstName; }
            set
            {
                Set(ref firstName, value);
                RaisePropertyChanged(() => FirstName);
            }
        }

        [Required(ErrorMessage = "Please, Enter a the Last Name of this Cardholder")]
        public string LastName
        {
            get { return lastName; }
            set
            {
                Set(ref lastName, value);
                RaisePropertyChanged(() => LastName);
            }
        }

        public string PreferredName
        {
            get { return preferredName; }
            set
            {
                Set(ref preferredName, value);
                RaisePropertyChanged(() => PreferredName);
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

        [EmailAddress(ErrorMessage = "Please, Enter a valid Email")]
        [Required(ErrorMessage = "Please, Enter a Valid Email for this Cardholder")]
        public string PrimaryEmail
        {
            get { return primaryEmail; }
            set
            {
                Set(ref primaryEmail, value);
                RaisePropertyChanged(() => PrimaryEmail);
            }
        }

        [EmailAddress(ErrorMessage = "Please, Enter a valid Email")]
        public string SecondaryEmail
        {
            get { return secondaryEmail; }
            set
            {
                Set(ref secondaryEmail, value);
                RaisePropertyChanged(() => SecondaryEmail);
            }
        }

        [Required(ErrorMessage = "Please, Enter a Valid ID Number for this Cardholder")]
        public string IDNo
        {
            get { return idNo; }
            set
            {
                Set(ref idNo, value);
                RaisePropertyChanged(() => IDNo);
            }
        }

        public City City
        {
            get { return city; }
            set
            {
                Set(ref city, value);
                RaisePropertyChanged(() => City);
            }
        }

        public State State
        {
            get { return state; }
            set
            {
                Set(ref state, value);
                RaisePropertyChanged(() => State);
            }
        }

        public Tenant TenantDetails
        {
            get { return tenantDetails; }
            set
            {
                Set(ref tenantDetails, value);
                RaisePropertyChanged(() => TenantDetails);
            }
        }

        public Parking ParkingDetails
        {
            get { return parkingDetails; }
            set
            {
                Set(ref parkingDetails, value);
                RaisePropertyChanged(() => ParkingDetails);
            }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                Set(ref dateOfBirth, value);
                RaisePropertyChanged(() => DateOfBirth);
            }
        }

        [Required(ErrorMessage = "Please, Enter a Phone Number")]
        [Phone(ErrorMessage = "Please, Enter a valid Phone Number")]
        public long HandPhoneNumber
        {
            get { return handPhoneNumber; }
            set
            {
                Set(ref handPhoneNumber, value);
                RaisePropertyChanged(() => HandPhoneNumber);
            }
        }

        [Phone(ErrorMessage = "Please, Enter a valid Phone Number")]
        public long WorkPhoneNumber
        {
            get { return workPhoneNumber; }
            set
            {
                Set(ref workPhoneNumber, value);
                RaisePropertyChanged(() => WorkPhoneNumber);
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

        public CardholderGender Gender
        {
            get { return gender; }
            set
            {
                Set(ref gender, value);
                RaisePropertyChanged(() => Gender);
            }
        }

        public IdentificationType IDType
        {
            get { return iDType; }
            set
            {
                Set(ref iDType, value);
                RaisePropertyChanged(() => IDType);
            }
        }

        #endregion
    }

    public enum CardholderGender
    {
        MALE,
        FEMALE
    }

    public enum IdentificationType
    {
        PASSPORT,
        NATIONAL_IC
    }

    public enum Status
    {
        ACTIVE,
        INACTIVE,
        DEACTIVATED
    }
}