using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.QuickBooksOnline.DataSourceHandlers.Static;

public class IncludeOnSendDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>
        {
            { "false", "False" },
            { "true", "True" }
        };
    }
}