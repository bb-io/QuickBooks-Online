using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Models.Requests.Customers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.DataSourceHandlers;

public class CustomerDataSource(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var customerActions = new CustomerActions(InvocationContext);
        
        var customersResponse = await customerActions.GetAllCustomers(new GetCustomerFilterRequest());
        
        return customersResponse.Customers
            .Where(x => context.SearchString == null ||
                        x.DisplayName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .Select(x => new DataSourceItem(x.Id, x.DisplayName));
    }
}