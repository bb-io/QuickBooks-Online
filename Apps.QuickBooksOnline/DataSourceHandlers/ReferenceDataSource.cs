using Apps.QuickBooksOnline.Models.Requests.Attachments;
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
            var invoiceDataHandler = new InvoiceDataHandler(InvocationContext);
            var invoices = await invoiceDataHandler.GetDataAsync(new DataSourceContext { SearchString = context.SearchString }, cancellationToken);
            
            return invoices;
        }
        
        throw new NotImplementedException($"Reference type {request.EntityType} is not implemented yet.");
    }
}