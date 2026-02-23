using QuestPDF.Infrastructure;
using System;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterestService.Application.DTO;

namespace InterestService.Repository.Repository
{
public class EmiSchedulePdf : IDocument
  {
    private readonly List<EmischeduleResponse> _schedule;
    private readonly decimal _loanAmount;
    private readonly decimal _interestRate;
        public EmiSchedulePdf(List<EmischeduleResponse> schedule, decimal loanAmount, decimal interestRate)
        {
            _schedule = schedule;
            _loanAmount = loanAmount;
            _interestRate = interestRate;
        }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
      container.Page(page =>
      {
        page.Margin(20);

        page.Header()
            .Text("Loan EMI Schedule")
            .FontSize(20)
            .Bold();

        page.Content().Column(col =>
        {
          col.Item().Text($"Loan Amount: {_loanAmount}");
          col.Item().Text($"Interest Rate: {_interestRate}%");
          col.Item().Table(table =>
          {
            table.ColumnsDefinition(columns =>
            {
              columns.ConstantColumn(50);
              columns.RelativeColumn();
              columns.RelativeColumn();
              columns.RelativeColumn();
              columns.RelativeColumn();
            });

            table.Header(header =>
            {
              header.Cell().Text("No").Bold();
              header.Cell().Text("Due Date").Bold();
              header.Cell().Text("EMI").Bold();
              header.Cell().Text("Principal").Bold();
              header.Cell().Text("Interest").Bold();
            });

            foreach (var item in _schedule)
            {
              table.Cell().Text(item.InstallmentNumber.ToString());
              table.Cell().Text(item.DueDate.ToShortDateString());
              table.Cell().Text(item.EmiAmount.ToString());
              table.Cell().Text(item.PrincipalComponent.ToString());
              table.Cell().Text(item.InterestComponent.ToString());
            }
          });
        });

        page.Footer()
            .AlignCenter()
            .Text(x =>
            {
              x.Span("Generated on ");
              x.Span(DateTime.Now.ToString("dd-MM-yyyy"));
            });
      });
    }
  }
}

