using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestService.Application.DTO
{
  public class PaymentDetailDTO
  {
    public int Month { get; set; }
    public double OpeningBalance { get; set; }
    public double EmiAmount { get; set; }
    public double InterestComponent { get; set; }
    public double PrincipalComponent { get; set; }
    public double ClosingBalance { get; set; }
  }
}
