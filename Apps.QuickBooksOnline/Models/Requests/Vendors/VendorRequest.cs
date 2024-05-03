using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Requests.Vendors;

public class VendorRequest
{
    [Display("Vendor ID")]
    public string VendorId { get; set; }
}