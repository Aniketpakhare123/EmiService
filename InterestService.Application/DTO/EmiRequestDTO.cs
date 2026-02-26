using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestService.Application.DTO
{
  public class EmiRequestDTO
  {
    public int loanId { get; set; }
  }

  public class UpdateEmiPaymentRequest
  {
    public decimal PaidAmount { get; set; }
    public decimal PaidInterest { get; set; }
    public DateTime PaidDate { get; set; }
  }
}
