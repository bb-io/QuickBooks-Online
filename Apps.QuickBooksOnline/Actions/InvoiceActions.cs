using RestSharp;
using Apps.QuickBooksOnline.Api.Models.Responses;
using Apps.QuickBooksOnline.Contracts;
using Apps.QuickBooksOnline.Models.Dtos;
using Apps.QuickBooksOnline.Models.Dtos.Invoices;
using Apps.QuickBooksOnline.Models.Requests.Invoices;
using Apps.QuickBooksOnline.Models.Responses.Invoices;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.Actions;

[ActionList]
public class InvoiceActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Get all invoices", Description = "Get all invoices")]
    public async Task<GetAllInvoicesResponse> GetAllInvoices()
    {
        var sql = "select * from Invoice";
        var invoicesWrapper =
            await Client.ExecuteWithJson<QueryInvoicesWrapper>($"/query?query={sql}", Method.Get, null, Creds);

        return new GetAllInvoicesResponse(invoicesWrapper.QueryResponse.Invoice);
    }

    [Action("Get invoice", Description = "Get an invoice by ID")]
    public async Task<GetInvoiceResponse> GetInvoice([ActionParameter] InvoiceRequest input)
    {
        var invoiceWrapper =
            await Client.ExecuteWithJson<InvoiceWrapper>($"/invoice/{input.InvoiceId}", Method.Get, null, Creds);
        return new GetInvoiceResponse(invoiceWrapper.Invoice);
    }

    [Action("Create invoice", Description = "Create an invoice with a customer and line items")]
    public async Task<GetInvoiceResponse> CreateInvoice([ActionParameter] CreateInvoiceRequest input)
    {
        var itemActions = new ItemActions(InvocationContext);

        var body = new Dictionary<string, object>
        {
            { "CustomerRef", new { value = input.CustomerId } }
        };

        var items = await itemActions.GetItemsByIds(input.ItemIds);
        var lines = new List<object>();
        for (var i = 0; i < items.Count; i++)
        {
            lines.Add(new
            {
                Amount = input.LineAmounts.ElementAt(i),
                DetailType = "SalesItemLineDetail",
                SalesItemLineDetail = new
                {
                    ItemRef = new
                    {
                        value = items[i].Id,
                        name = items[i].Name
                    }
                }
            });
        }
        
        body.Add("Line", lines);

        try
        {
            var invoiceWrapper = await Client.ExecuteWithJson<InvoiceWrapper>("/invoice", Method.Post, body, Creds);
            return new GetInvoiceResponse(invoiceWrapper.Invoice);
        }
        catch (Exception)
        {
            var serializedBody = Newtonsoft.Json.JsonConvert.SerializeObject(body); // test
            throw new Exception($"Error creating invoice, body: {serializedBody}");
        }
    }

    [Action("Update invoice", Description = "Update an invoice with a new due date and class reference")]
    public async Task<GetInvoiceResponse> UpdateInvoice([ActionParameter] UpdateInvoiceRequest input)
    {
        var syncToken = await GetSyncTokenAsync(input.InvoiceId, input.SyncToken);
        var data = new Dictionary<string, object>
        {
            { "Id", input.InvoiceId },
            { "SyncToken", syncToken }
        };

        if (!string.IsNullOrEmpty(input.ClassReferenceId))
        {
            data.Add("ClassRef", new
            {
                value = input.ClassReferenceId
            });
        }
        
        if (input.DueDate.HasValue)
        {
            data.Add("DueDate", input.DueDate.Value.ToString("yyyy-MM-dd"));
        }

        var invoiceWrapper = await Client.ExecuteWithJson<InvoiceWrapper>("/invoice", Method.Post, data, Creds);
        return new GetInvoiceResponse(invoiceWrapper.Invoice);
    }

    [Action("Delete invoice", Description = "Delete an invoice")]
    public async Task DeleteInvoice([ActionParameter] DeleteInvoiceRequest input)
    {
        var syncToken = await GetSyncTokenAsync(input.InvoiceId, input.SyncToken);
        
        var data = new Dictionary<string, object>
        {
            { "Id", input.InvoiceId },
            { "SyncToken", syncToken }
        };

        if (!string.IsNullOrWhiteSpace(input.ClassReferenceId))
        {
            if (string.IsNullOrEmpty(input.ClassReferenceName))
            {
                data.Add("ClassRef", new
                {
                    value = input.ClassReferenceId
                });
            }
            else
            {
                data.Add("ClassRef", new
                {
                    name = input.ClassReferenceName,
                    value = input.ClassReferenceId
                });
            }
        }

        await Client.ExecuteWithJson<object>($"/invoice/{input.InvoiceId}?operation=delete", Method.Post, data, Creds);
    }

    [Action("Send invoice", Description = "Send an invoice to billing email address or email provided in request")]
    public async Task<GetInvoiceResponse> SendInvoice([ActionParameter] SendInvoiceRequest request)
    {
        var endpoint = $"/invoice/{request.InvoiceId}/send";
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            endpoint += $"?sendTo={request.Email}";
        }

        var wrapper = await Client.ExecuteWithJson<InvoiceWrapper>(endpoint, Method.Post, null, Creds);
        return new GetInvoiceResponse(wrapper.Invoice);
    }

    [Action("Void invoice", Description = "Void an invoice")]
    public async Task<GetInvoiceResponse> VoidInvoice([ActionParameter] VoidInvoiceRequest request)
    {
        var syncToken = await GetSyncTokenAsync(request.InvoiceId, request.SyncToken);
        var data = new Dictionary<string, object>
        {
            { "Id", request.InvoiceId },
            { "SyncToken", syncToken }
        };

        if (!string.IsNullOrWhiteSpace(request.ClassReferenceId))
        {
            if (string.IsNullOrEmpty(request.ClassReferenceName))
            {
                data.Add("ClassRef", new
                {
                    value = request.ClassReferenceId
                });
            }
            else
            {
                data.Add("ClassRef", new
                {
                    name = request.ClassReferenceName,
                    value = request.ClassReferenceId
                });
            }
        }

        var wrapper = await Client.ExecuteWithJson<InvoiceWrapper>($"/invoice/{request.InvoiceId}?operation=void",
            Method.Post, data, Creds);
        return new GetInvoiceResponse(wrapper.Invoice);
    }
    
    private async Task<string> GetSyncTokenAsync(string invoiceId, string? syncToken)
    {
        if (string.IsNullOrWhiteSpace(syncToken))
        {
            return (await GetInvoice(new InvoiceRequest { InvoiceId = invoiceId })).SyncToken;
        }

        return syncToken;
    }
}