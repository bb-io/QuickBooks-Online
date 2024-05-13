using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Webhooks.Models.Responses;

public class OnVendorsDeletedResponse
{
    [Display("Vendor IDs")]
    public IEnumerable<string> VendorIds { get; set; }
}