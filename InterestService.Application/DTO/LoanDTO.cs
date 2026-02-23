using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestService.Application.DTO
{


  public class LoanApiResponse {
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public LoanDTO Data { get; set; }
    public object Errors { get; set; }
    public Meta Meta { get; set; }
  }
  public class Meta {
    public DateTime timestamp { get; set; }
  }

  public  class LoanDTO
  {
    public int LoanId { get; set; }
    public int DealId { get; set; }
    public int SanctionId { get; set; }
    public int DisbursementId { get; set; }
    public int CustomerId { get; set; }
    public int ScorecardId { get; set; }
    public int LoanTypeId { get; set; }
    public int? BranchId { get; set; }
    public string? SanctionNo { get; set; }
    public decimal SanctionedAmount { get; set; }
    public decimal InterestRate { get; set; }
    public int TenureMonths { get; set; }
    public decimal EmiAmount { get; set; }
    public decimal DisbursedAmount { get; set; }
    public DateTime DisbursementDate { get; set; }
    public string? TransactionReference { get; set; }

    public DateTime dueDate { get; set; }
    public int? scheduleId { get; set; }
    public int EmiDay { get; set; }
    public DateTime MaturityDate { get; set; }
    public DateTime FirstEmiDate { get; set; }
    public decimal OutstandingPrincipal { get; set; }
    public decimal OutstandingInterest { get; set; }
    public decimal TotalOutstanding { get; set; }
    public int RemainingTenure { get; set; }
    public int Dpd { get; set; }
    public string AccountStatus { get; set; }
    public DateTime? LastPaymentDate { get; set; }
    public DateTime? NextEmiDate { get; set; }
    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string CreatedBy { get; set; }

    public decimal eligibleAmount { get; set; }
    public string deletedBy { get; set; }

  }
}
