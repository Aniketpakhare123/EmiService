using InterestService.Application.DTO;
using InterestService.Application.Interfaces;
using InterestService.Domain.Model;
using InterestService.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestService.Repository.Repository
{
  public class EmiRepo : IEmi
  {
    EmiClient emiClient;
    private readonly ApplicationDbContext _context;
    public EmiRepo(EmiClient client,ApplicationDbContext context) {
      this.emiClient = client;
      this._context = context;
    }


    public  async Task<List<EmischeduleResponse>> GenerateSchedule(EmiRequestDTO req)  
    {
      var n = await emiClient.getLoanDetails(req.loanId);
      if (n == null) { return null; }
      var schedule = new List<EmischeduleResponse>();

      decimal monthlyRate = n.InterestRate / 12 / 100;

      decimal emi = n.DisbursedAmount * monthlyRate *
                   (decimal)Math.Pow((double)(1 + monthlyRate), n.TenureMonths)
                   / ((decimal)Math.Pow((double)(1 + monthlyRate), n.TenureMonths) - 1);

      emi = Math.Round(emi, 2);

      decimal balance = n.DisbursedAmount;

      for (int i = 1; i <= n.TenureMonths; i++)
      {
        decimal interest = Math.Round(balance * monthlyRate, 2);
        decimal principal = Math.Round(emi - interest, 2);
        decimal endingBalance = Math.Round(balance - principal, 2);

        if (i == n.TenureMonths) 
        {
          principal = balance;
          emi = principal + interest;
          endingBalance = 0;
        }


        schedule.Add(new EmischeduleResponse
        {
          ScheduleId = i,
          InstallmentNumber = i,
          DueDate = n.DisbursementDate.AddMonths(i),
          OpeningBalance = balance,
          EmiAmount = emi,
          PrincipalComponent = principal,
          InterestComponent = interest,
          ClosingBalance = endingBalance,
          PaymentStatus = null,
          PaidAmount = 0,
          PenaltyAmount =0,
          PendingAmount = principal,
        });

        balance = endingBalance;


        var loanEmiSchedule = new LoanEmiSchedule
        {
          
          loanId = n.LoanId,
          InstallmentNo = i,
          Duedate = int.Parse(n.DisbursementDate.AddMonths(i).ToString("yyyyMMdd")), 
          EmiAmount = emi,
          PrincipalAmount = principal,
          InterestAmount = interest,
          PaymentStatus = "pending",
          PaidAmount = 0,
          PaidDate = null,
          PendingAmount = principal,
          PenaltyAmount = 0,
          TotalDue = emi, 
          paidInterest = null,
          isActive = true,
          createdAt = DateTime.UtcNow.ToString("o")
        };
        
        await _context.EmiSchedules.AddAsync(loanEmiSchedule);
        _context.SaveChanges();

      }
      return schedule;
    }



  }
}
