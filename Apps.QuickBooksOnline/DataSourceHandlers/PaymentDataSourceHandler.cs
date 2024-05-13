using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Models.Responses;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.DataSourceHandlers;

public class PaymentDataSourceHandler(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var paymentActions = new PaymentActions(InvocationContext);
        
        var customersResponse = await paymentActions.GetAllPayments();
        
        return customersResponse.Payments
            .Where(x => context.SearchString == null ||
                        x.CustomerName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.Id, GetReadableName);
    }
    
    private string GetReadableName(PaymentResponse response)
    {
        return $"[{response.Id}] - {response.TotalAmount}";
    }
}