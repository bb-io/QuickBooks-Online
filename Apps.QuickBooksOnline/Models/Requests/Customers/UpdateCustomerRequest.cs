using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests.Customers;

public class UpdateCustomerRequest : CreateCustomerRequest
{
    [Display("Customer ID"), DataSource(typeof(CustomerDataSource))]
    public string CustomerId { get; set; }
    
    [Display("Sync token", Description = "If not provided, we will get customer's current sync token.")]
    public string? SyncToken { get; set; }
    
    public bool? Job { get; set; }

    public string? Domain { get; set; }

    public bool? Active { get; set; }
}