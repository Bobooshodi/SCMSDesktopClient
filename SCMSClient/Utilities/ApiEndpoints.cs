namespace SCMSClient.Utilities
{
    public static class ApiEndpoints
    {
        public static string login = "api/oauth/token";

        //Card Service
        public static string FindCardById = "cards";

        public static string AllCards = "cards";
        public static string CreateCard = "cards";
        public static string UpdateCard = "cards";
        public static string DeleteCard = "cards";

        // Card Type Service
        public static string FindCardTypeById = "card-types";

        public static string AllCardTypes = "card-types";
        public static string CreateCardType = "card-types";
        public static string UpdateCardType = "card-types";
        public static string DeleteCardType = "card-types";

        // Card Vendor Service
        public static string FindCardVendorById = "vendors";

        public static string AllCardVendors = "vendors";
        public static string CreateCardVendor = "vendors";
        public static string UpdateCardVendor = "vendors";
        public static string DeleteCardVendor = "vendors";

        // Card Request Service
        public static string FindCardRequestById = "card-requests";

        public static string AllCardRequests = "card-requests";
        public static string CreateCardRequest = "card-requests";
        public static string UpdateCardRequest = "card-requests";
        public static string DeleteCardRequest = "card-requests";

        // Cardholders
        public static string FindCardholderById = "cardholders";

        public static string AllCardholders = "cardholders";
        public static string CreateCardholder = "cardholders";
        public static string UpdateCardholder = "cardholders";
        public static string DeleteCardholder = "cardholders";

        // Employees
        public static string FindEmployeeById = "employees";

        public static string AllEmployees = "employees";
        public static string CreateEmployee = "employees";
        public static string UpdateEmployee = "employees";
        public static string DeleteEmployee = "employees";

        // Tenants
        public static string FindTenantById = "tenants";

        public static string AllTenants = "tenants";
        public static string CreateTenant = "tenants";
        public static string UpdateTenant = "tenants";
        public static string DeleteTenant = "tenants";

        // Card Replacement Service
        public static string FindCardReplacementRequestById = "card-replacement-requests";

        public static string AllCardReplacementRequests = "card-replacement-requests";
        public static string CreateCardReplacementRequest = "card-replacement-requests";
        public static string UpdateCardReplacementRequest = "card-replacement-requests";
        public static string DeleteCardReplacementRequest = "card-replacement-requests";

        // Personalization Requests Service
        public static string FindPersonalizationRequestById = "personalization-requests";

        public static string AllPersonalizationRequests = "personalization-requests";
        public static string CreatePersonalizationRequest = "personalization-requests";
        public static string UpdatePersonalizationRequest = "personalization-requests";
        public static string DeletePersonalizationRequest = "personalization-requests";
    }
}