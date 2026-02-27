using LoanOriginationService.Application.DTO.Customer;
using LoanOriginationService.Application.DTO.External;
using LoanOriginationService.Application.Helper;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ScoreCardController : ControllerBase
{
    private readonly CustomerClient _customerClient;
    private readonly ScorecardClient _scorecardClient;

    public ScoreCardController(
        CustomerClient customerClient,
        ScorecardClient scorecardClient)
    {
        _customerClient = customerClient;
        _scorecardClient = scorecardClient;
    }

    // 🔹 Get Name Only
    [HttpGet("customer/{id}")]
    public async Task<IActionResult> GetScoreCardByCustomer(int id)
    {
        var result = await _customerClient.GetCustomerDetailsById(id);

        if (result == null)
            return NotFound(new { message = "Customer not found" });

        return Ok(new
        {
            success = true,
            data = result
        });
    }

    // 🔹 Get Full Scorecard (CIBIL + Eligible)
    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetScorecard(int customerId)
    {
        var data = await _scorecardClient
            .GetFullScorecardByCustomerId(customerId);

        return Ok(ApiResponse<ScoreCardDto>
            .SuccessResponse(data, "success"));
    }
}