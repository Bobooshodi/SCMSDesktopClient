using Bogus;
using SCMSClient.Models;
using System;
using System.Collections.Generic;

namespace SCMSClient.Utilities
{
    public static class RandomDataGenerator
    {
        public static List<Cardholder> Cardholders(int amount)
        {
            string[] genders = { "Male", "Female" };

            var testUsers = new Faker<Cardholder>()
                //Basic rules using built-in generators
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Address, f => f.Address.StreetAddress())
                .RuleFor(u => u.City, f => f.Address.City())
                .RuleFor(u => u.Country, f => f.Address.Country())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.Nationality, f => f.Address.Country())
                .RuleFor(u => u.DateOfBirth, f => f.Person.DateOfBirth)
                .RuleFor(u => u.Gender, f => f.PickRandom(genders))
                .RuleFor(u => u.IdentificationType, f => f.PickRandom<IdentificationType>())
                .RuleFor(u => u.IdentificationNo, f => f.Random.Replace("###-##-####"))
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumberFormat())
                .RuleFor(u => u.SmartCardId, f => f.Random.Replace("#####-##-####"))
                .RuleFor(u => u.State, f => f.Address.State())
                .RuleFor(u => u.Status, f => f.PickRandom<CardUserStatus>())
                .RuleFor(u => u.ID, Guid.NewGuid().ToString());

            return testUsers.Generate(amount);
        }

        public static List<Card> Cards(int amount)
        {
            return null;
        }

        public static List<CardVendor> CardVendors(int amount)
        {
            var vendors = new Faker<CardVendor>()
                .RuleFor(v => v.Address, f => f.Address.StreetAddress())
                .RuleFor(v => v.Description, f => f.Company.CatchPhrase())
                .RuleFor(v => v.ID, Guid.NewGuid().ToString())
                .RuleFor(v => v.Name, f => f.Company.CompanyName())
                .RuleFor(v => v.Phone, f => f.Phone.PhoneNumber());

            return vendors.Generate(amount);
        }

        public static List<CarPark> CarParks(int amount)
        {
            var bayTypes = new[] { "Personal", "Visitor", "Company" };

            var carParks = new Faker<CarPark>()
                .RuleFor(c => c.BayNo, f => f.Random.Number(100).ToString())
                .RuleFor(c => c.ID, Guid.NewGuid().ToString())
                .RuleFor(c => c.BayType, f => f.PickRandom(bayTypes))
                .RuleFor(c => c.Building, f => f.Address.BuildingNumber())
                .RuleFor(c => c.Cardholder, f => f.Name.FindName())
                .RuleFor(c => c.CardholderId, Guid.NewGuid().ToString())
                .RuleFor(c => c.CardParkId, Guid.NewGuid().ToString())
                .RuleFor(c => c.StartDate, f => f.Date.Past())
                .RuleFor(c => c.EndDate, (f, u) => f.Date.Future(f.Random.Number(2), u.StartDate))
                .RuleFor(c => c.RequestId, Guid.NewGuid().ToString())
                .RuleFor(c => c.Status, f => f.PickRandom<CarParkStatus>());

            return carParks.Generate(amount);
        }

        public static List<Vehicle> Vehicles(int amount)
        {
            var vehicles = new Faker<Vehicle>()
                .RuleFor(c => c.Brand, f => f.Commerce.Product())
                .RuleFor(c => c.CardholderId, Guid.NewGuid().ToString())
                .RuleFor(c => c.ID, Guid.NewGuid().ToString())
                .RuleFor(c => c.NumberPlate, f => f.Random.Replace("??? ####"))
                .RuleFor(c => c.VehicleModel, f => f.Commerce.ProductName());

            return vehicles.Generate(amount);
        }
    }
}
