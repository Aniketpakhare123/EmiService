using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestService.Domain.Model
{
  public  class InterestAccurals
  {
   
    public int AccrualId { get; set; }
    
    public int LoanId { get; set; }
    public int LoanAccount { get; set; }
    public DateTime AccrualDate { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal PrincipalBalance { get; set; }
    [Column(TypeName = "decimal(5,2)")]
    public decimal InterestRate { get; set; }
    [Column(TypeName = "decimal(8,6)")]
    public decimal DailyInterestRate { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal AccruedInterest { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal CumulativeInterest { get; set; }
    public string AccrualType { get; set; }

    public string CalculationMethod { get; set; } 
    public int DaysInMonth { get; set; }
    public string AccrualStatus { get; set; }
    public string? Remarks { get; set; }
  }
}
