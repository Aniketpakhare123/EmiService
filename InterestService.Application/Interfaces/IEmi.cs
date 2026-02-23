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

  }
}
