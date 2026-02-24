using LoanOriginationService.Application.DTO.Customer;
using LoanOriginationService.Application.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Application.DTO.Officer
{
    public class OfficerClient
    {
        private readonly HttpClient client;

        public OfficerClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<OfficerDto>> GetOfficerDetails()
        {
            var response = await client
                .GetFromJsonAsync<ApiResponse<List<OfficerDto>>>(
                    "/api/Auth/Officer");

            Console.WriteLine("Officer API returned: " + (response?.Data?.Count ?? 0));

            return response?.Data ?? new List<OfficerDto>();
        }
    }
}
