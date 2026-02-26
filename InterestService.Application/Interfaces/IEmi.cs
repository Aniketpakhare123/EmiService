using InterestService.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestService.Application.Interfaces
{
  public interface IEmi
  {
    Task<List<EmischeduleResponse>> GenerateSchedule(EmiRequestDTO req);
    Task<List<EmischeduleResponse>> GetEmiScheduleByLoanId(int loanId);
    Task UpdateEmiAfterPayment(int loanId, int? scheduleId, decimal paidAmount, decimal paidInterest, DateTime paidDate);
  }
}
