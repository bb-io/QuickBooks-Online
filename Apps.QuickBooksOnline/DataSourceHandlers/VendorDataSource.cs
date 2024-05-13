using Apps.QuickBooksOnline.Actions;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.DataSourceHandlers;

public class VendorDataSource(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var customerActions = new VendorActions(InvocationContext);
        
        var customersResponse = await customerActions.GetAllVendors();
        
        return customersResponse.Vendors
            .Where(x => context.SearchString == null ||
                        x.DisplayName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.Id, x => x.DisplayName);
    }
}