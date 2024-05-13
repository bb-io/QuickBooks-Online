using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests.Invoices;

public class InvoiceRequest : SyncTokenRequest
{
    [Display("Invoice ID"), DataSource(typeof(InvoiceDataHandler))]
    public string InvoiceId { get; set; }
}