using Apps.QuickBooksOnline.Api.Models.Responses;
using Apps.QuickBooksOnline.Models.Dtos;

namespace Apps.QuickBooksOnline.Models.Responses.Invoices;

public class GetAllInvoicesResponse
{
    public List<GetInvoiceResponse> Invoices { get; set; }

    public GetAllInvoicesResponse(List<InvoiceDto> invoiceDtos)
    {
        Invoices = invoiceDtos.Select(invoiceDto => new GetInvoiceResponse(invoiceDto)).ToList();
    }
}