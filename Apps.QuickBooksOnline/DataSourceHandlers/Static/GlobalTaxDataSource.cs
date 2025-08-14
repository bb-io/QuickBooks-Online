using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.DataSourceHandlers.Static
{
    public class GlobalTaxDataSource : IStaticDataSourceItemHandler
    {
        public IEnumerable<DataSourceItem> GetData()
        {
            return new List<DataSourceItem>
            {
                new("TaxExcluded", "Tax excluded"),
                new("TaxInclusive", "Tax inclusive"),
                new("NotApplicable", "Not applicable")
            };
        }
    }
}
