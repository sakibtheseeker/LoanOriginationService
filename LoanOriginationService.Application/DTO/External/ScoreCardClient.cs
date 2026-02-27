using LoanOriginationService.Application.DTO.External;
using LoanOriginationService.Application.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Application.DTO.Customer
{
    public class ScorecardClient
    {
        private readonly HttpClient client;

        public ScorecardClient(HttpClient client)
        {
            this.client = client;
        }



        public async Task<ScorecardCustomerDto?>
    GetScorecardCustomerById(int id)
        {
            var response = await client
                .GetFromJsonAsync<ApiResponse<ScorecardCustomerDto>>(
                    $"api/scorecard/customer/{id}");

            return response?.Data;
        }

        public async Task<ScoreCardDto?> GetFullScorecardByCustomerId(int id)
        {
            var response = await client
                .GetFromJsonAsync<ApiResponse<ScoreCardDto>>(
                    $"api/ScoreCard/customer/{id}");

            return response?.Data;
        }
    }
}
