using InterestService.Application.DTO;
using InterestService.Application.Interfaces;
using InterestService.Domain.Model;
using InterestService.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterestService.Repository.Repository
{
  public class EmiRepo : IEmi
  {
    EmiClient emiClient;
    private readonly ApplicationDbContext _context;

    public EmiRepo(EmiClient client, ApplicationDbContext context)
    {
      this.emiClient = client;
      this._context = context;
    }

    public async Task<List<EmischeduleResponse>> GenerateSchedule(EmiRequestDTO req)
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
        decimal openingBalance = balance;
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
          OpeningBalance = openingBalance,
          EmiAmount = emi,
          PrincipalComponent = principal,
          InterestComponent = interest,
          ClosingBalance = endingBalance,
          PaymentStatus = null,
          PaidAmount = 0,
          PenaltyAmount = 0,
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
          OpeningBalance = openingBalance,
          ClosingBalance = endingBalance,
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

    public async Task<List<EmischeduleResponse>> GetEmiScheduleByLoanId(int loanId)
    {
      var schedules = await _context.EmiSchedules
          .Where(e => e.loanId == loanId && e.isActive)
          .OrderBy(e => e.InstallmentNo)
          .ToListAsync();

      return schedules.Select(e => new EmischeduleResponse
      {
        ScheduleId = e.id,
        LoanId = e.loanId,
        InstallmentNumber = e.InstallmentNo,
        OpeningBalance = e.OpeningBalance,
        ClosingBalance = e.ClosingBalance,
        EmiAmount = e.EmiAmount,
        PrincipalComponent = e.PrincipalAmount,
        InterestComponent = e.InterestAmount,
        PaymentStatus = e.PaymentStatus,
        PaidAmount = e.PaidAmount,
        PaidDate = e.PaidDate,
        PendingAmount = e.PendingAmount,
        PenaltyAmount = e.PenaltyAmount,
        TotalDue = e.TotalDue,
      }).ToList();
    }

    public async Task UpdateEmiAfterPayment(int loanId, int? scheduleId, decimal paidAmount, decimal paidInterest, DateTime paidDate)
    {
      LoanEmiSchedule emi;

      if (scheduleId.HasValue)
        emi = await _context.EmiSchedules.FindAsync(scheduleId.Value);
      else
        emi = await _context.EmiSchedules
            .Where(e => e.loanId == loanId && e.PaymentStatus == "pending" && e.isActive)
            .OrderBy(e => e.InstallmentNo)
            .FirstOrDefaultAsync();

      if (emi == null) return;

      emi.PaidAmount += paidAmount;
      emi.paidInterest = (emi.paidInterest ?? 0) + paidInterest;
      emi.PaidDate = paidDate;
      emi.PendingAmount = Math.Max(0, emi.TotalDue - emi.PaidAmount);
      emi.PaymentStatus = emi.PendingAmount <= 0 ? "paid" : "partial";

      await _context.SaveChangesAsync();
    }
  }
}
