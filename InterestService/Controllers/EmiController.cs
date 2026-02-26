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
    private readonly IEmi emiservice;
    private readonly EmiClient emiClient;

    public EmiController(IEmi e, EmiClient emiClient)
    {
      emiservice = e;
      this.emiClient = emiClient;
    }

    [HttpPost("generate-emi")]
    public async Task<IActionResult> GenerateEmi([FromBody] EmiRequestDTO request)
    {
      var result = await emiservice.GenerateSchedule(request);
      return Ok(result);
    }

    [HttpGet("schedule/{loanId}")]
    public async Task<IActionResult> GetEmiSchedule(int loanId)
    {
      var result = await emiservice.GetEmiScheduleByLoanId(loanId);
      return Ok(result);
    }

    [HttpPut("{loanId}/after-payment")]
    public async Task<IActionResult> UpdateEmiAfterPayment(int loanId, [FromQuery] int? scheduleId, [FromBody] UpdateEmiPaymentRequest request)
    {
      await emiservice.UpdateEmiAfterPayment(loanId, scheduleId, request.PaidAmount, request.PaidInterest, request.PaidDate);
      return Ok(new { message = "EMI schedule updated after payment." });
    }

    [HttpGet("emi-pdf/{loanId}")]
    public async Task<IActionResult> DownloadEmiPdf(int loanId)
    {
      var schedule = await emiservice.GetEmiScheduleByLoanId(loanId);
      var loan = await emiClient.getLoanDetails(loanId);

      if (loan == null)
        return NotFound(new { message = "Loan not found." });

      var document = new EmiSchedulePdf(schedule, loan.DisbursedAmount, loan.InterestRate);
      var pdfBytes = document.GeneratePdf();
      return File(pdfBytes, "application/pdf", $"EMI_Schedule_{loanId}.pdf");
    }
  }
}
