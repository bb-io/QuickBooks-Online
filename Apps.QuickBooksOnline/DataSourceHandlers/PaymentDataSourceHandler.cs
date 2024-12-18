using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Models.Responses;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.DataSourceHandlers;

public class PaymentDataSourceHandler(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var paymentActions = new PaymentActions(InvocationContext);
        
        var customersResponse = await paymentActions.GetAllPayments();
        
        return customersResponse.Payments
            .Where(x => context.SearchString == null ||
                        x.CustomerName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .Select(x => new DataSourceItem(x.Id, GetReadableName(x)));
    }
    
    private string GetReadableName(PaymentResponse response)
    {
        return $"[{response.Id}] - {response.TotalAmount}";
    }
}