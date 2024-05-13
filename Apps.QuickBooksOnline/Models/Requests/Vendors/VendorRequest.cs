using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests.Vendors;

public class VendorRequest
{
    [Display("Vendor ID"), DataSource(typeof(VendorDataSource))]
    public string VendorId { get; set; }
}