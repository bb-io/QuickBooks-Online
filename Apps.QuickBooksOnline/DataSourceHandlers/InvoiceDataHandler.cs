﻿using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Api.Models.Responses;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.DataSourceHandlers;

public class InvoiceDataHandler(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var invoiceActions = new InvoiceActions(InvocationContext);
        
        var customersResponse = await invoiceActions.GetAllInvoices();
        
        return customersResponse.Invoices
            .Where(x => context.SearchString == null ||
                        BuildReadableName(x).Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.InvoiceId, BuildReadableName);
    }
    
    private string BuildReadableName(GetInvoiceResponse invoice)
    {
        return $"[{invoice.InvoiceId}] - {invoice.CustomerName}";
    }
}