using LoanOriginationService.Application.DTO.LoanDeals;
using LoanOriginationService.Application.Helper;
using LoanOriginationService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanOriginationService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanDealsController : ControllerBase
    {
        private readonly IloanDealsRepo repo;
        public LoanDealsController(IloanDealsRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllLoanDeals()
        {
            var data = await repo.GetAllLoanDeals();
            if (data != null)
            {
                return Ok(ApiResponse<List<LoanDealsDto>>.SuccessResponse(
               data, "Loan Details Fetched Successfully"));
            }
            return Ok(ApiResponse<LoanDealsDto>.FailureResponse(
              "Data Unavailable", "No records found.", "404"));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FetchLoanDealsById(int id)
        {
            var data = await repo.GetLoanDealsById(id);
            if (data != null)
            {
                return Ok(ApiResponse<LoanDealsDto>.SuccessResponse(
                    data, "Loan Details Fetched Successfully"));
            }

            return Ok(ApiResponse<LoanDealsDto>.FailureResponse(
             "Data Unavailable", "No records found.", "404"));
        }

        [HttpPost]
        public async Task <IActionResult> AddLoanDeald(LoanDealsResponseDto dto)
        {
            await repo.AddLoanDealsAsync(dto);
            return Ok(ApiResponse<string>.SuccessResponse(
                null, "Loan Deal Added Successfully"));
        }

        [HttpGet("customers/{id}")]
        public async Task<IActionResult> FetchLoanDealsByCustomerId(int id)
        {
            var data = await repo.GetLoanDealsByCustId(id);

            if (data != null)
            {
                return Ok(ApiResponse<LoanDealsDto>.SuccessResponse(
                    data, "Loan Details Fetched Successfully"));
            }
            return Ok(ApiResponse<LoanDealsDto>.FailureResponse(
             "Data Unavailable", "No records found.", "404"));
        }


        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveLoanDeal(int id, LoanDecisionDto e)
        {
            await repo.ApproveLoanDeal(id, e);
            return Ok(ApiResponse<string>.SuccessResponse(
                null, "Loan Approved Successfully"));
        }



        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectLoanDeal(int id, LoanDecisionDto e)
        {
            await repo.RejectLoanDeal(id, e);

            return Ok(ApiResponse<string>.SuccessResponse(
                null, "Loan Rejected Successfully"));
        }
    }
}
