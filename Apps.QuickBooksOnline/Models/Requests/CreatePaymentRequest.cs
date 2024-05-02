using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests;

public class CreatePaymentRequest
{
    [Display("Total amount")]
    public string TotalAmount { get; set; }

    [Display("Customer ID"), DataSource(typeof(CustomerDataSource))]
    public string? CustomerId { get; set; }

    [Display("Job ID")]
    public string? JobId { get; set; }
}