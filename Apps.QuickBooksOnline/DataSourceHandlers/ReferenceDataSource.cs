using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Api.Models.Responses;
using Apps.QuickBooksOnline.Models.Requests.Attachments;
using Apps.QuickBooksOnline.Models.Requests.Invoices;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.DataSourceHandlers;

public class ReferenceDataSource(InvocationContext invocationContext, [ActionParameter] CreateAttachmentRequest request)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.EntityType))
        {
            throw new InvalidOperationException("You should select a reference type first.");
        }

        if (request.EntityType == "Invoice")
        {
            var invoiceActions = new InvoiceActions(InvocationContext, null);
            var customersResponse = await invoiceActions.GetAllInvoices(new InvoiceFilterRequest());
        
            return customersResponse.Invoices
                .Where(x => context.SearchString == null ||
                            BuildReadableName(x).Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Take(20)
                .ToDictionary(x => x.InvoiceId, BuildReadableName);
        }
        
        throw new NotImplementedException($"Reference type {request.EntityType} is not implemented yet.");
    }
    
    private string BuildReadableName(GetInvoiceResponse invoice)
    {
        return $"[{invoice.InvoiceId}] - {invoice.CustomerName}";
    }
}