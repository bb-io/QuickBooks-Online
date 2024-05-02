using RestSharp;
using Apps.QuickBooksOnline.Api.Models.Responses;
using Apps.QuickBooksOnline.Contracts;
using Apps.QuickBooksOnline.Models.Dtos;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.Actions;

[ActionList]
public class InvoiceActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Get invoice", Description = "Get an invoice")]
    public async Task<GetInvoiceResponse> GetInvoice([ActionParameter] GetInvoiceParameters input)
    {
        var invoiceWrapper = await Client.ExecuteWithJson<InvoiceWrapper>($"/invoice/{input.InvoiceId}", Method.Get, null, Creds);
        return new GetInvoiceResponse(invoiceWrapper.Invoice);
    }
    
    [Action("Create an invoice", Description = "Create an invoice")]
    public async Task<GetInvoiceResponse> CreateInvoice([ActionParameter] CreateInvoiceParameters input)
    {
        var body = new
        {
            Line = new[]
            {
                new
                {
                    DetailType = "SalesItemLineDetail",
                    Amount = input.LineAmount,
                    SalesItemLineDetail = new
                    {
                        ItemRef = new
                        {
                            name = input.ItemName,
                            value = input.ItemValue
                        }
                    }
                }
            },
            CustomerRef = new
            {
                value = input.CustomerId
            }
        };
        
        var invoiceWrapper = await Client.ExecuteWithJson<InvoiceWrapper>("/invoice", Method.Post, body, Creds);
        return new GetInvoiceResponse(invoiceWrapper.Invoice);
    }

    [Action("Update an invoice", Description = "Sparse update an invoice")]
    public async Task<GetInvoiceResponse> UpdateInvoice([ActionParameter] UpdateInvoiceParameters input)
    {
        var body = new
        {
            SyncToken = input.SyncToken ?? "0",
            Id = input.InvoiceId,
            sparse = true,
            DueDate = input.DueDate.ToString("yyyy-MM-dd")
        };
        
        var invoiceWrapper = await Client.ExecuteWithJson<InvoiceWrapper>("/invoice", Method.Post, body, Creds);
        return new GetInvoiceResponse(invoiceWrapper.Invoice);
    }
    
    [Action("Delete an invoice", Description = "Delete an invoice")]
    public async Task DeleteInvoice(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] DeleteInvoiceParameters input)
    {
        var body = new
        {
            SyncToken = input.SyncToken ?? "0",
            Id = input.InvoiceId
        };
        
        await Client.ExecuteWithJson<object>($"/invoice/{input.InvoiceId}?operation=delete", Method.Post, body, Creds);
    }
}