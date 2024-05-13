using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests.Invoices;

public class SendInvoiceRequest : InvoiceRequest
{
    [Display("Email address")]
    public string? Email { get; set; }
}