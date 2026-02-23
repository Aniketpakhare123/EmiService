using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestService.Domain.Model
{
  public class LoanEmiSchedule
  {
    [Key]
    public int  id { get; set; }
    public int loanId { get; set; }
    public int InstallmentNo { get; set; }
    public int Duedate { get; set; }
    public decimal EmiAmount { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public string PaymentStatus { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime? PaidDate { get; set; }
    public decimal PendingAmount { get; set; }
    public decimal PenaltyAmount { get; set; }
    public decimal TotalDue { get; set; }

    public decimal? paidInterest { get; set; }

    public bool isActive { get; set; }

    public string createdAt { get; set; }


  }
}
