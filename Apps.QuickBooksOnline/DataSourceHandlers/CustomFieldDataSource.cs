using Apps.QuickBooksOnline.Models.Dtos;
using Apps.QuickBooksOnline.Models.Requests.Invoices;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.QuickBooksOnline.DataSourceHandlers;

public class CustomFieldDataSource(InvocationContext invocationContext, [ActionParameter] GetCustomFieldRequest request)
    : AppInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if(string.IsNullOrWhiteSpace(request.InvoiceId))
            throw new Exception("You should input an Invoice ID first.");
        
        var invoiceWrapper =
            await Client.ExecuteWithJson<InvoiceWrapper>($"/invoice/{request.InvoiceId}", Method.Get, null, Creds);
        
        return invoiceWrapper.Invoice.CustomField
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .Select(x => new DataSourceItem(x.DefinitionId, x.Name));
    }
}