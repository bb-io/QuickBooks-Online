using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests.Invoices;

public class UpdateInvoiceRequest : InvoiceRequest
{
    [Display("Customer ID")]
    public string? CustomerId { get; set; }

    [Display("Item IDs")]
    public IEnumerable<string>? ItemIds { get; set; }
    
    [Display("Line amounts")]
    public IEnumerable<double>? LineAmounts { get; set; }
    
    [Display("Class reference ID"), DataSource(typeof(ClassDataHandler))]
    public string? ClassReferenceId { get; set; }
    
    [Display("Due date")]
    public DateTime? DueDate { get; set; }
}