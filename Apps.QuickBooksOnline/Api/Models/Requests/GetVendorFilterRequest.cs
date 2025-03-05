using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Api.Models.Requests
{
    public class GetVendorFilterRequest
    {
        [Display("Display name")]
        public string? DisplayName { get; set; }

        [Display("Given name")]
        public string? GivenName { get; set; }

        [Display("Company name")]
        public string? CompanyName { get; set; }

        [Display("Last updated time")]
        public DateTime? LastUpdatedTime { get; set; }
    }
}
