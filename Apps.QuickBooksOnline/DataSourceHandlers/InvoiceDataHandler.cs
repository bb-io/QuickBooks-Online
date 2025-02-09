﻿using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Api.Models.Responses;
using Apps.QuickBooksOnline.Models.Requests.Invoices;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.DataSourceHandlers;

public class InvoiceDataHandler(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var invoiceActions = new InvoiceActions(InvocationContext, null);
        
        var customersResponse = await invoiceActions.GetAllInvoices(new InvoiceFilterRequest());
        
        return customersResponse.Invoices
            .Where(x => context.SearchString == null ||
                        BuildReadableName(x).Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .Select(x => new DataSourceItem(x.InvoiceId, BuildReadableName(x)));
    }
    
    private string BuildReadableName(GetInvoiceResponse invoice)
    {
        return $"[{invoice.InvoiceId}] - {invoice.CustomerName}";
    }
}