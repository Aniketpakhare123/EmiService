using InterestService.Application.DTO;
using InterestService.Application.Interfaces;
using InterestService.Repository.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterestService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmiController : ControllerBase
  {

    IEmi emiservice;

    public EmiController(IEmi e) {
      emiservice = e;
    }

    [HttpPost("generate-emi")]
    public async Task<IActionResult> GenerateEmi([FromBody] EmiRequestDTO request)
    {
      var result =await emiservice.GenerateSchedule(request);
      return Ok(result);
    }

    [HttpGet("emi-pdf/{loanId}")]
    public async Task<IActionResult> DownloadEmiPdf(int loanId)
    {
      var schedule = await emiservice.GetEmiScheduleByLoanId(loanId);
      var loan = await _loanService.GetLoanById(loanId);

      var document = new EmiSchedulePdf(schedule, loan.Amount, loan.InterestRate);

      var pdfBytes = document.GeneratePdf();

      return File(pdfBytes, "application/pdf", $"EMI_Schedule_{loanId}.pdf");
    }
  }
}
