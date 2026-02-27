using Azure;
using LoanOriginationService.Application.DTO.LoanDeals;
using LoanOriginationService.Application.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Application.DTO.Customer
{
    public class CustomerClient
    {
        private readonly HttpClient client;

        public CustomerClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<CustomerDto?> GetCustomerDetailsById(int id)
        {
            var response = await client
                .GetFromJsonAsync<ApiResponse<CustomerDto>>(
                    $"api/Customer/{id}");

            return response?.Data;
        }
    }

}
