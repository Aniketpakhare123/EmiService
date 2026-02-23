using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestService.Application.DTO
{
  public class EmischeduleResponse
  {
  
    public int ScheduleId { get; set; }
    public int LoanId { get; set; }
    public int InstallmentNumber { get; set; }
    public DateTime DueDate { get; set; }
    public decimal EmiAmount { get; set; }
    public decimal PrincipalComponent { get; set; }
    public decimal InterestComponent { get; set; }
    public decimal OpeningBalance { get; set; }
    public decimal ClosingBalance { get; set; }
    public string PaymentStatus { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime? PaidDate { get; set; }
    public decimal PendingAmount { get; set; }
    public decimal PenaltyAmount { get; set; }
    public decimal TotalDue { get; set; }
    public int DaysOverdue { get; set; }
  }
}
