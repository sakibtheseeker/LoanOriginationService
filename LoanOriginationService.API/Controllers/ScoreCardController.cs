using LoanOriginationService.Application.DTO.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanOriginationService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreCardController : ControllerBase
    {
        private readonly CustomerClient _client;

        public ScoreCardController(CustomerClient client)
        {
            _client = client;
        }

        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetScoreCardByCustomer(int id)
        {
            var result = await _client.GetCustomerDetailsById(id);

            if (result == null)
                return NotFound(new { message = "Scorecard not found" });

            return Ok(new
            {
                success = true,
                data = result
            });
        }
    }
    }
