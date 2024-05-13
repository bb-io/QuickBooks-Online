using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Webhooks.Models.Responses;

public class OnCustomersDeletedResponse
{
    [Display("Customer Ids")]
    public IEnumerable<string> CustomerIds { get; set; }
}