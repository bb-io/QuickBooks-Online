using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Contracts
{
    public class CustomerRequest
    {
        [Display("Customer ID"), DataSource(typeof(CustomerDataSource))]
        public string CustomerId { get; set; }
    }
}
