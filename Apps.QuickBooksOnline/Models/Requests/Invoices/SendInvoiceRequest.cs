using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Requests.Invoices;

public class SendInvoiceRequest : InvoiceRequest
{
    [Display("Email address")]
    public string? Email { get; set; }
}