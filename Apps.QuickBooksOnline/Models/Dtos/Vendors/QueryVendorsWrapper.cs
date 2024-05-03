using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Vendors;

public class QueryVendorsWrapper
{
    
    [JsonProperty("QueryResponse")]
    public VendorsWrapper QueryResponse { get; set; }
}

public class VendorsWrapper
{
    [JsonProperty("Invoice")]
    public List<VendorDto> Vendor { get; set; }
}