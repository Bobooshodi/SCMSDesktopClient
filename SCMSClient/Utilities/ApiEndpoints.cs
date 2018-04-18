namespace SCMSClient.Utilities
{
    public static class ApiEndpoints
    {
        public static string login = "api/oauth/token";

        //Card Service
        public static string FindCardById = "api/cards/get";

        public static string AllCards = "api/cards/get/all";
        public static string CreateCard = "api/cards/create";
        public static string UpdateCard = "api/cards/update";
        public static string DeleteCard = "api/cards/delete";

        // Card Type Service
        public static string FindCardTypeById = "api/card-types/get";

        public static string AllCardTypes = "api/card-types/get/all";
        public static string CreateCardType = "api/card-types/create";
        public static string UpdateCardType = "api/card-types/update";
        public static string DeleteCardType = "api/card-types/delete";

        // Card Vendor Service
        public static string FindCardVendorById = "api/card-vendors/get";

        public static string AllCardVendors = "api/card-vendors/get/all";
        public static string CreateCardVendor = "api/card-vendors/create";
        public static string UpdateCardVendor = "api/card-vendors/update";
        public static string DeleteCardVendor = "api/card-vendors/delete";

        // Card Request Service
        public static string FindCardRequestById = "api/card-requests/get";

        public static string AllCardRequests = "api/card-requests/get/all";
        public static string CreateCardRequest = "api/card-requests/create";
        public static string UpdateCardRequest = "api/card-requests/update";
        public static string DeleteCardRequest = "api/card-requests/delete";

        // Cardholders
        public static string FindCardholderById = "api/cardholders/get";

        public static string AllCardholders = "api/cardholders/get/all";
        public static string CreateCardholder = "api/cardholders/create";
        public static string UpdateCardholder = "api/cardholders/update";
        public static string DeleteCardholder = "api/cardholders/delete";

        // Employees
        public static string FindEmployeeById = "api/employees/get";

        public static string AllEmployees = "api/employees/get/all";
        public static string CreateEmployee = "api/employees/create";
        public static string UpdateEmployee = "api/employees/update";
        public static string DeleteEmployee = "api/employees/delete";

        // Tenants
        public static string FindTenantById = "api/tenants/get";

        public static string AllTenants = "api/tenants/get/all";
        public static string CreateTenant = "api/tenants/create";
        public static string UpdateTenant = "api/tenants/update";
        public static string DeleteTenant = "api/tenants/delete";

        // Card Replacement Service
        public static string FindCardReplacementRequestById = "api/card-replacements";

        public static string AllCardReplacementRequests = "api/card-replacements/get/all";
        public static string CreateCardReplacementRequest = "api/card-replacements/create";
        public static string UpdateCardReplacementRequest = "api/card-replacement/updte";
        public static string DeleteCardReplacementRequest = "api/card-replacement/delete";

        // Personalization Requests Service
        public static string FindPersonalizationRequestById = "api/personalization-requests/get";

        public static string AllPersonalizationRequests = "api/personalisation-requests/get/all";
        public static string CreatePersonalizationRequest = "api/personalisation-requests/create";
        public static string UpdatePersonalizationRequest = "api/personalisation-requests/update";
        public static string DeletePersonalizationRequest = "api/personalisation-requests/delete";
    }
}