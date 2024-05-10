using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Requests.Invoices;

public class CreateInvoiceRequest
{
    [Display("Customer ID")]
    public string CustomerId { get; set; }

    [Display("Item IDs")]
    public IEnumerable<string> ItemIds { get; set; }
    
    [Display("Line amounts")]
    public IEnumerable<double> LineAmounts { get; set; }
}