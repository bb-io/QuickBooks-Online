using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.QuickBooksOnline.DataSourceHandlers.Static;

public class ReferenceTypeDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>
        {
            { "Invoice", "Invoice" }
        };
    }
}