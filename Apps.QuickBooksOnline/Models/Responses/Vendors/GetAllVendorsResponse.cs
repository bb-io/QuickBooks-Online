using Apps.QuickBooksOnline.Models.Dtos.Vendors;

namespace Apps.QuickBooksOnline.Models.Responses.Vendors;

public class GetAllVendorsResponse
{
    public List<VendorResponse> Vendors { get; set; }
    
    public GetAllVendorsResponse(List<VendorDto> vendors)
    {
        Vendors = vendors.Select(vendor => new VendorResponse(vendor)).ToList();
    }
}