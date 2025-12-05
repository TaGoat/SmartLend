using Microsoft.AspNetCore.Mvc;
using SmartLend.Application.DTOs;
using SmartLend.Application.Services;
using SmartLend.Domain.Entities;

namespace SmartLend.Api.Controllers;

[ApiController]
[Route("api/loans")]
public class LoanController : ControllerBase
{
    private readonly LoanService _service;

    public LoanController(LoanService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitLoan([FromBody] LoanRequestDto request)
    {
        var result = await _service.ProcessLoan(request);

        return Ok(new 
        { 
            Id = result.Id,
            Status = result.Status.ToString(),
            Reason = result.DecisionReason,
            Note = "Check Terminal for the AI Advice!"
        });
    }
}