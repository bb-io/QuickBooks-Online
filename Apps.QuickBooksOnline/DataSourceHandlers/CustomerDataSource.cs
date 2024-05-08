using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Models.Requests.Customers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.DataSourceHandlers;

public class CustomerDataSource(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var customerActions = new CustomerActions(InvocationContext);
        
        var customersResponse = await customerActions.GetAllCustomers(new GetCustomerFilterRequest());
        
        return customersResponse.Customers
            .Where(x => context.SearchString == null ||
                        x.DisplayName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.Id, x => x.DisplayName);
    }
}