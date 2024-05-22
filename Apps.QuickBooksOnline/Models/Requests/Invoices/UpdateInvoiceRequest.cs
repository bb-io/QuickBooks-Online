using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests.Invoices;

public class UpdateInvoiceRequest : InvoiceRequest
{
    [Display("Customer ID"), DataSource(typeof(CustomerDataSource))]
    public string CustomerId { get; set; }

    [Display("Item IDs"), DataSource(typeof(ItemDataSourceHandler))]
    public IEnumerable<string>? ItemIds { get; set; }

    [Display("Quantities", Description = "Quantities for each line item, by default 1")]
    public IEnumerable<int>? Quantities { get; set; }
    
    [Display("Unit prices", Description = "Unit prices for each line item. For example, specify 0.4 for 40% tax.")]
    public IEnumerable<string>? UnitPrices { get; set; }

    [Display("Class IDs", Description = "Class references for each line item")]
    public IEnumerable<string>? ClassIds { get; set; }

    public IEnumerable<string>? Descriptions { get; set; }
    
    [Display("Line amounts")]
    public IEnumerable<double> LineAmounts { get; set; }

    [Display("Invoice date")]
    public DateTime? InvoiceDate { get; set; }
    
    [Display("Due date")]
    public DateTime? DueDate { get; set; }
    
    [Display("Class ID"), DataSource(typeof(ClassDataHandler))]
    public string? ClassId { get; set; }

    public UpdateInvoiceRequest()
    { }

    public UpdateInvoiceRequest(CreateInvoiceRequest createInvoiceRequest)
    {
        CustomerId = createInvoiceRequest.CustomerId;
        ItemIds = createInvoiceRequest.ItemIds;
        Quantities = createInvoiceRequest.Quantities;
        UnitPrices = createInvoiceRequest.UnitPrices;
        ClassIds = createInvoiceRequest.ClassIds;
        Descriptions = createInvoiceRequest.Descriptions;
        LineAmounts = createInvoiceRequest.LineAmounts;
        InvoiceDate = createInvoiceRequest.InvoiceDate;
        ClassId = createInvoiceRequest.ClassId;
    }
}