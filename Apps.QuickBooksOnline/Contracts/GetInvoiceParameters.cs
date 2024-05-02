using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Contracts;

public class GetInvoiceParameters
{
    [Display("Invoice ID")]
    public string InvoiceId { get; set; }
}