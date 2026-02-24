using LoanOriginationService.Application.DTO.LoanDeals;
using LoanOriginationService.Application.DTO.LoanTypes;
using LoanOriginationService.Application.Helper;
using LoanOriginationService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanOriginationService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanTypesController : ControllerBase
    {
        private readonly ILoanRepo repo;

        public LoanTypesController(ILoanRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetloantypeById(int id)
        {
            LoanDto data = await repo.GetLoanTypeById(id);
            if (data != null)
            {
                return Ok(ApiResponse<LoanDto>.SuccessResponse(
               data, "Loan Type Fetched Successfully"));
            }
            return Ok(ApiResponse<LoanDto>.FailureResponse(
             "Data Unavailable", "No records found.", "404"));
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllLoanTypes(
            [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            var result = await repo.GetAllLoanTypes(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Addloantypes(LoanTypesResponseDto dto)
        {
            repo.AddLoanType(dto);
            return Ok(ApiResponse<string>.SuccessResponse(
               null, "Loan Type Added Successfully"));
        }
    }
}
