using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Requests.Customers;

public class GetCustomerFilterRequest
{
    [Display("Display name")]
    public string? DisplayName { get; set; }
    
    [Display("Given name")]
    public string? GivenName { get; set; }

    [Display("Last updated time", Description = "By default, it will be set to 2015-03-01")]
    public DateTime? LastUpdatedTime { get; set; }
}