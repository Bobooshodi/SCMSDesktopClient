using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCMSClient.Models
{
    public class Cardholder : BaseModel
    {
        private string identificationNumber;

        [Required(ErrorMessage = "Please, Enter the First Name of this Cardholder")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please, Enter the First Name of this Cardholder")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [Required(ErrorMessage = "Please, Enter the Date of Birth of this Cardholder")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please, Enter the Gender of this Cardholder")]
        public string Gender { get; set; }

        public string SmartCardId { get; set; }

        public IdentificationType IdentificationType { get; set; }

        [Required(ErrorMessage = "Please, Enter either a Passport or IC Number for this Cardholder")]
        public string IdentificationNo
        {
            get => identificationNumber;
            set
            {
                Set(ref identificationNumber, value);
                RaisePropertyChanged(() => IdentificationNo);
            }
        }

        public CardUserStatus Status { get; set; }

        [Required(ErrorMessage = "Please, Enter the Nationality of this Cardholder")]
        public string Nationality { get; set; }

        [Phone(ErrorMessage = "Please, Enter a valid Phone Number")]
        [Required(ErrorMessage = "Please, Enter the Phone number of this Cardholder")]
        public string Phone { get; set; }

        [Phone(ErrorMessage = "Please, Enter a valid Phone Number")]
        public string WorkPhone { get; set; }

        [EmailAddress(ErrorMessage = "Please, Enter a valid Email Address")]
        [Required(ErrorMessage = "Please, Enter the Email Address of this Cardholder")]
        public string Email { get; set; }

        [EmailAddress(ErrorMessage = "Please, Enter a valid Email Address")]
        public string WorkEmail { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        public List<Card> Cards { get; set; }

        public List<CarPark> CarParks { get; set; }

        public List<Vehicle> Vehicles { get; set; }
    }
    public enum CardUserStatus
    {
        Active,
        Inactive,
        Blacklist
    }
    public enum IdentificationType
    {
        NRIC,
        Passport
    }
}