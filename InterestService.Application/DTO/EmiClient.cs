using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InterestService.Application.DTO
{
  public class EmiClient            
  {
    private readonly HttpClient http;

    public EmiClient(HttpClient http) {
      this.http = http;
    }

    public async Task <LoanDTO> getLoanDetails(int id)
    {
      { try
        {
          var response = await http.GetFromJsonAsync<LoanApiResponse>($"/api/Loan/{id}");
          if (response == null)
          {
            Console.WriteLine("Response was null.");
            return null; }
          if (response.Data == null)
          {
            Console.WriteLine("Response.Data was null.");
            return null; }

           return response.Data;
        } catch (HttpRequestException httpEx)
        {
          Console.WriteLine($"HTTP Error: {httpEx.Message}");
          throw; }
        catch (NotSupportedException nsEx)
        { Console.WriteLine($"Content type not supported: {nsEx.Message}")
            ; throw; }
        catch (JsonException jsonEx)
        { Console.WriteLine($"JSON parse error: {jsonEx.Message}");
          throw; } catch (Exception e)
        { Console.WriteLine($"Unexpected error: {e.Message}");
          throw; } }
    }

  }
}
