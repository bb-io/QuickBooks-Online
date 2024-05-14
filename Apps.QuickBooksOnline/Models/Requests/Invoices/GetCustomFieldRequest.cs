using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests.Invoices;

public class GetCustomFieldRequest : InvoiceRequest
{
    [Display("Custom field ID"), DataSource(typeof(CustomFieldDataSource))]
    public string CustomFieldId { get; set; }
}