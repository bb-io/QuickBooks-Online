using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests.Invoices;

public class CreateInvoiceRequest
{
    [Display("Customer ID"), DataSource(typeof(CustomerDataSource))]
    public string CustomerId { get; set; }

    [Display("Item IDs"), DataSource(typeof(ItemDataSourceHandler))]
    public IEnumerable<string> ItemIds { get; set; }
    
    [Display("Line amounts")]
    public IEnumerable<double> LineAmounts { get; set; }
}