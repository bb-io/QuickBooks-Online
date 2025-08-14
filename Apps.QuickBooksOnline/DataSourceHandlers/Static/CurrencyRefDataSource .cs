using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.DataSourceHandlers.Static
{
    public class CurrencyRefDataSource : IStaticDataSourceItemHandler
    {
        public IEnumerable<DataSourceItem> GetData()
        {
            return new List<DataSourceItem>
            {
                new("USD", "US Dollar (USD)"),
                new("EUR", "Euro (EUR)"),
                new("GBP", "British Pound (GBP)"),
                new("CAD", "Canadian Dollar (CAD)"),
                new("AUD", "Australian Dollar (AUD)"),
                new("JPY", "Japanese Yen (JPY)"),
                new("CHF", "Swiss Franc (CHF)"),
                new("CNY", "Chinese Yuan (CNY)"),
                new("HKD", "Hong Kong Dollar (HKD)"),
                new("NZD", "New Zealand Dollar (NZD)"),
            };
        }
    }
}
