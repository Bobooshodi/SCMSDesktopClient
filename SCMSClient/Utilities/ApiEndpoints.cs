namespace SCMSClient.Utilities
{
    public static class ApiEndpoints
    {
        public static string login = "api/oauth/token";

        //Card Service
        public static string FindCardById = "api/cards/get/";

        public static string AllCards = "api/cards/get/all";
        public static string CreateCard = "api/cards/create";
        public static string UpdateCard = "api/cards/update";
        public static string DeleteCard = "api/cards/delete/";

        // Card Type Service
        public static string FindCardTypeById = "api/card-types/get/";

        public static string AllCardTypes = "api/card-types/get/all";
        public static string CreateCardType = "api/card-types/create";
        public static string UpdateCardType = "api/card-types/update";
        public static string DeleteCardType = "api/card-types/delete/";

        // Card Vendor Service
        public static string FindCardVendorById = "api/card-vendors/get/";

        public static string AllCardVendors = "api/card-vendors/get/all";
        public static string CreateCardVendor = "api/card-vendors/create";
        public static string UpdateCardVendor = "api/card-vendors/update";
        public static string DeleteCardVendor = "api/card-vendors/delete/";

        // Card Request Service
        public static string FindCardRequestById = "api/card-requests/get/";

        public static string AllCardRequests = "api/card-requests/get/all";
        public static string CreateCardRequest = "api/card-requests/create";
        public static string UpdateCardRequest = "api/card-requests/update";
        public static string DeleteCardRequest = "api/card-requests/delete/";

        // Cardholders
        public static string FindCardholderById = "api/cardholders/get/";

        public static string AllCardholders = "api/cardholders/get/all";
        public static string CreateCardholder = "api/cardholders/create";
        public static string UpdateCardholder = "api/cardholders/update";
        public static string DeleteCardholder = "api/cardholders/delete/";

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
        public static string FindCardReplacementRequestById = "api/card-replacements/";

        public static string AllCardReplacementRequests = "api/card-replacements/get/all";
        public static string CreateCardReplacementRequest = "api/card-replacements/create";
        public static string UpdateCardReplacementRequest = "api/card-replacement/updte";
        public static string DeleteCardReplacementRequest = "api/card-replacement/delete/";

        // Personalization Requests Service
        public static string FindPersonalizationRequestById = "api/personalization-requests/get/";

        public static string AllPersonalizationRequests = "api/personalization-requests/get/all";
        public static string CreatePersonalizationRequest = "api/personalization-requests/create";
        public static string UpdatePersonalizationRequest = "api/personalization-requests/update";
        public static string DeletePersonalizationRequest = "api/personalization-requests/delete/";
    }
}