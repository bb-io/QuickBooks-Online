using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Contracts
{
    public class CreateInvoiceParameters
    {
        [Display("Customer ID")]
        public string CustomerId { get; set; }

        [Display("Line amount")]
        public double LineAmount { get; set; }
        
        [Display("Item name")]
        public string ItemName { get; set; }
        
        [Display("Item value")]
        public string ItemValue { get; set; }
    }
}
