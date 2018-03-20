using Bogus;
using CardEncoderLib;
using SCMSClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCMSClient.Utilities
{
    public static class RandomDataGenerator
    {
        public static List<CardKey> CardKeys(int amount, int startSector, char keyType)
        {
            var keys = new List<CardKey>();

            for (int i = startSector; i <= (startSector + amount); i++)
            {
                var cardKeys = new Faker<CardKey>()
                .RuleFor(k => k.Key, f => f.Random.AlphaNumeric(12).ToUpper())
                .RuleFor(k => k.Block, 0)
                .RuleFor(k => k.Sector, i)
                .RuleFor(k => k.KeyType, keyType);

                keys.Add(cardKeys.Generate());
            }

            return keys;
        }

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
                .RuleFor(u => u.UserType, f => f.PickRandom<SHCCardType>())
                .RuleFor(u => u.ID, Guid.NewGuid().ToString())
                .RuleFor(u => u.Cards, Cards(1))
                .RuleFor(u => u.CarParks, f => CarParks(f.Random.Number(0, 3)))
                .RuleFor(u => u.Vehicles, f => Vehicles(f.Random.Int(0, 3)));

            return testUsers.Generate(amount);
        }

        public static List<Employee> Employees(int amount)
        {
            string[] genders = { "Male", "Female" };

            var testUsers = new Faker<Employee>()
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
                .RuleFor(u => u.UserType, f => SHCCardType.Employee)
                .RuleFor(u => u.ID, Guid.NewGuid().ToString())
                .RuleFor(u => u.Cards, Cards(1))
                .RuleFor(u => u.EmployeeId, Guid.NewGuid().ToString())
                .RuleFor(U => U.ContractId, f => f.Random.Replace("#####-???-***"))
                .RuleFor(u => u.Designation, f => f.Name.JobTitle())
                .RuleFor(u => u.Company, Companies(1).FirstOrDefault())
                .RuleFor(u => u.Buildings, f => Buildings(f.Random.Int(0, 3)))
                .RuleFor(u => u.CarParks, f => CarParks(f.Random.Number(0, 3)))
                .RuleFor(u => u.Vehicles, f => Vehicles(f.Random.Int(0, 3)));

            return testUsers.Generate(amount);
        }

        public static List<Tenant> Tenants(int amount)
        {
            string[] genders = { "Male", "Female" };

            var testUsers = new Faker<Tenant>()
                //Basic rules using built-in generators
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Address, f => f.Address.StreetAddress())
                .RuleFor(u => u.City, f => f.Address.City())
                .RuleFor(u => u.Country, f => f.Address.Country())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.Nationality, (f, u) => u.Country)
                .RuleFor(u => u.DateOfBirth, f => f.Person.DateOfBirth)
                .RuleFor(u => u.Gender, f => f.PickRandom(genders))
                .RuleFor(u => u.IdentificationType, f => f.PickRandom<IdentificationType>())
                .RuleFor(u => u.IdentificationNo, f => f.Random.Replace("###-##-####"))
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumberFormat())
                .RuleFor(u => u.SmartCardId, f => f.Random.Replace("#####-##-####"))
                .RuleFor(u => u.State, f => f.Address.State())
                .RuleFor(u => u.Status, f => f.PickRandom<CardUserStatus>())
                .RuleFor(u => u.ID, Guid.NewGuid().ToString())
                .RuleFor(u => u.Cards, Cards(1))
                .RuleFor(u => u.CarParks, (f) => CarParks(f.Random.Int(0, 3)))
                .RuleFor(u => u.SHCTenant, SHCTenants(1).FirstOrDefault())
                .RuleFor(u => u.Buildings, f => Buildings(f.Random.Int(0, 1)))
                .RuleFor(u => u.UserType, SHCCardType.Tenant)
                .RuleFor(u => u.Vehicles, f => Vehicles(f.Random.Int(0, 3)));

            return testUsers.Generate(amount);
        }

        public static List<SHCTenant> SHCTenants(int amount)
        {
            var testTenants = new Faker<SHCTenant>()
                .RuleFor(t => t.Name, f => f.Company.CompanyName())
                .RuleFor(t => t.AccessGroups, f => null)
                .RuleFor(t => t.Address, f => f.Address.StreetAddress())
                .RuleFor(t => t.Building, f => f.Commerce.Department())
                .RuleFor(t => t.BuildingId, Guid.NewGuid().ToString())
                .RuleFor(t => t.Code, f => f.Random.Replace("#####-#######-###############"))
                .RuleFor(t => t.ContactPerson, f => f.Name.FindName())
                .RuleFor(t => t.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(t => t.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(t => t.ID, Guid.NewGuid().ToString())
                .RuleFor(t => t.ImageUrl, f => null)
                .RuleFor(t => t.IsActive, f => f.Random.Bool());

            return testTenants.Generate(amount);
        }

        public static List<Building> Buildings(int amount)
        {
            var testBuildings = new Faker<Building>()
                .RuleFor(b => b.AccessGroups, f => null)
                .RuleFor(b => b.Address, f => f.Address.FullAddress())
                .RuleFor(b => b.ContactPerson, f => f.Name.FindName())
                .RuleFor(b => b.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(b => b.ID, Guid.NewGuid().ToString())
                .RuleFor(b => b.Image, f => f.Image.City())
                .RuleFor(b => b.Name, f => f.Company.CompanyName())
                .RuleFor(b => b.Phone, f => null);

            return testBuildings.Generate(amount);
        }

        public static List<Card> Cards(int amount)
        {
            var cardTypes = new[] { "Tenant", "Individual", "Employee", "Strata" };
            var cardVendors = CardVendors(5);
            var cards = new Faker<Card>()
                .RuleFor(c => c.BatchNo, f => f.Date.Recent(3).ToString("ddMMyyyy_HHmmss"))
                .RuleFor(c => c.CardInventoryNo, f => f.Random.Replace("###############"))
                .RuleFor(c => c.CardType, f => f.PickRandom(cardTypes))
                .RuleFor(c => c.CardVendor, f => cardVendors[f.Random.Number(3)])
                .RuleFor(c => c.ID, Guid.NewGuid().ToString())
                .RuleFor(c => c.MifareId, f => f.Random.AlphaNumeric(6))
                .RuleFor(c => c.StartDate, f => f.Date.Recent(7))
                .RuleFor(c => c.Status, f => f.PickRandom<CardStatus>());

            return cards.Generate(amount);
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

        public static List<CardType> CardTypes()
        {
            return new List<CardType>
            {
                new CardType
                {
                    ID = Guid.NewGuid().ToString(),
                    IsPermanent = false,
                    Name = "Individual Cards",
                    SHCCardType = SHCCardType.Individual
                },
                new CardType
                {
                    ID = Guid.NewGuid().ToString(),
                    IsPermanent = false,
                    Name = "Tenant Cards",
                    SHCCardType = SHCCardType.Tenant
                },
                new CardType
                {
                    ID = Guid.NewGuid().ToString(),
                    IsPermanent = false,
                    Name = "Strata Cards",
                    SHCCardType = SHCCardType.Strata
                },
                new CardType
                {
                    ID = Guid.NewGuid().ToString(),
                    IsPermanent = false,
                    Name = "Employee Cards",
                    SHCCardType = SHCCardType.Employee
                }
            };
        }

        public static List<BusinessUnit> BusinessUnits(int amount)
        {
            var rnd = new Random();

            var businessUnits = new Faker<BusinessUnit>()
                .RuleFor(b => b.Name, f => f.Name.JobType())
                .RuleFor(b => b.ID, Guid.NewGuid().ToString())
                .RuleFor(b => b.CardTypes, f => f.Random.ListItems(CardTypes())) // RandomArrayEntries(CardTypes(), rnd.Next(3)))
                .RuleFor(b => b.Companies, Companies(rnd.Next(5)))
                .RuleFor(b => b.Description, f => f.Lorem.Word());

            return businessUnits.Generate(amount);
        }

        public static List<Company> Companies(int amount)
        {
            var rnd = new Random();

            var companies = new Faker<Company>()
                .RuleFor(b => b.Name, f => f.Company.CompanyName())
                .RuleFor(b => b.ID, Guid.NewGuid().ToString())
                .RuleFor(b => b.BusinessUnits, f => null)
                .RuleFor(b => b.Code, f => f.Random.Replace("????-####-????"));

            return companies.Generate(amount);
        }

        public static List<SOACardRequest> CardRequests(int amount)
        {
            var data = new Faker<SOACardRequest>()
                .RuleFor(d => d.BusinessUnit, f => f.Commerce.Department())
                .RuleFor(d => d.BusinessUnitId, Guid.NewGuid().ToString())
                .RuleFor(d => d.CardType, f => f.PickRandom(CardTypes()))
                .RuleFor(d => d.ID, Guid.NewGuid().ToString())
                .RuleFor(d => d.Quantity, f => f.Random.Int(100))
                .RuleFor(d => d.RequestDate, f => f.Date.Recent(30))
                .RuleFor(d => d.RequestedBy, f => f.Name.FindName())
                .RuleFor(d => d.RequestedById, Guid.NewGuid().ToString())
                .RuleFor(d => d.RequestId, Guid.NewGuid().ToString());

            return data.Generate(amount);
        }

        public static List<SOAPersonalizationRequest> PersonalizationRequests(int amount)
        {
            var data = new Faker<SOAPersonalizationRequest>()
                .RuleFor(d => d.Cardholder, f => f.Name.FindName())
                .RuleFor(d => d.CardholderId, Guid.NewGuid().ToString())
                .RuleFor(d => d.CardInventoryNo, f => f.Random.Number(100).ToString())
                .RuleFor(d => d.CardType, f => f.PickRandom(CardTypes()))
                .RuleFor(d => d.ContractNo, f => f.Phone.PhoneNumber())
                .RuleFor(d => d.ID, Guid.NewGuid().ToString())
                .RuleFor(d => d.IdentificationNo, f => f.Random.AlphaNumeric(11))
                .RuleFor(d => d.IdentificationType, f => f.PickRandom<IdentificationType>())
                .RuleFor(d => d.PersonalizationStatus, f => f.PickRandom<RequestStatus>())
                .RuleFor(d => d.RequestDate, f => f.Date.Recent())
                .RuleFor(d => d.RequestId, Guid.NewGuid().ToString());

            return data.Generate(amount);
        }

        public static List<SOAReplaceCardRequest> ReplaceCardRequests(int amount)
        {
            var data = new Faker<SOAReplaceCardRequest>()
                .RuleFor(d => d.Cardholder, f => f.Name.FindName())
                .RuleFor(d => d.CardholderId, Guid.NewGuid().ToString())
                .RuleFor(d => d.CardId, f => f.Random.Int().ToString("D4"))
                .RuleFor(d => d.ID, Guid.NewGuid().ToString())
                .RuleFor(d => d.ReplacementReason, f => f.Lorem.Sentence())
                .RuleFor(d => d.RequestedDate, f => f.Date.Past())
                .RuleFor(d => d.RequestStatus, RequestStatus.New);

            return data.Generate(amount);
        }

        public static List<T> RandomArrayEntries<T>(List<T> arrayItems, int count)
        {
            var listToReturn = new List<T>();

            if (arrayItems.Count != count)
            {
                var deck = CreateShuffledDeck(arrayItems);

                for (var i = 0; i < count; i++)
                {
                    var item = deck.Pop();
                    listToReturn.Add(item);
                }

                return listToReturn;
            }

            return arrayItems;
        }

        public static Stack<T> CreateShuffledDeck<T>(IEnumerable<T> values)
        {
            var random = new Random();
            var list = new List<T>(values);
            var stack = new Stack<T>();
            while (list.Count > 0)
            {  // Get the next item at random.
                var randomIndex = random.Next(0, list.Count);
                var randomItem = list[randomIndex];
                // Remove the item from the list and push it to the top of the deck.
                list.RemoveAt(randomIndex);
                stack.Push(randomItem);
            }
            return stack;
        }
    }
}