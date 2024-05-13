using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Contracts
{
    public class CreateInvoiceParameters
    {
        [Display("Customer ID"), DataSource(typeof(CustomerDataSource))]
        public string CustomerId { get; set; }

        [Display("Line amount")]
        public double LineAmount { get; set; }
        
        [Display("Item name")]
        public string ItemName { get; set; }
        
        [Display("Item value")]
        public string ItemValue { get; set; }
    }
}
