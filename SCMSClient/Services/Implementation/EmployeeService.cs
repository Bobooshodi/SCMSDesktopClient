﻿using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace SCMSClient.Services.Implementation
{
    public class EmployeeService : AbstractService<Employee>, IEmployeeService
    {
        private readonly ICardholderService cardholderService;

        public EmployeeService(IHTTPService httpService, ICardholderService _cardholderService) : base(_httpService: httpService)
        {
            cardholderService = _cardholderService;

            getUrl = ApiEndpoints.FindEmployeeById;
            getAllUrl = ApiEndpoints.AllEmployees;
            updateUrl = ApiEndpoints.UpdateEmployee;
            createUrl = ApiEndpoints.CreateEmployee;
            deleteUrl = ApiEndpoints.DeleteEmployee;
        }
    }
}