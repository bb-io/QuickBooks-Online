using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Webhooks.Models.Responses;

public class OnInvoicesDeletedResponse
{
    [Display("Invoice IDs")]
    public IEnumerable<string> InvoiceIds { get; set; }
}