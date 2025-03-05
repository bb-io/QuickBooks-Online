using Apps.QuickBooksOnline.Models.Dtos.Vendors;
using Apps.QuickBooksOnline.Models.Responses.Vendors;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Api.Models.Responses
{
    public class GetVendorResponse
    {
        [Display("Vendor")]
        public VendorResponse Vendor { get; set; }

        public GetVendorResponse(VendorDto dto)
        {
            Vendor = new VendorResponse(dto);
        }
    }
}
