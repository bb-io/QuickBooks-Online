using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Contracts
{
    public class CreateCustomerRequest
    {
        [Display("Middle name")]
        public string MiddleName { get; set; }
    }
}
