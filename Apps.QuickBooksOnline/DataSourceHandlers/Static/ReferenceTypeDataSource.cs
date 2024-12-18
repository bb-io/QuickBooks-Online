using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.DataSourceHandlers.Static;

public class ReferenceTypeDataSource : IStaticDataSourceItemHandler
{
    IEnumerable<DataSourceItem> IStaticDataSourceItemHandler.GetData()
    {
        return new List<DataSourceItem>()
        {
            new ("Invoice", "Invoice")
        };
    }
}