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

    [Action("Create invoice", Description = "Create an invoice with a single line item and a customer reference")]
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

    [Action("Update invoice", Description = "Update an invoice with a new due date and class reference")]
    public async Task<GetInvoiceResponse> UpdateInvoice([ActionParameter] UpdateInvoiceParameters input)
    {
        var data = new Dictionary<string, object>
        {
            { "Id", input.InvoiceId },
            { "DueDate", input.DueDate.ToString("yyyy-MM-dd") },
            { "SyncToken", !string.IsNullOrWhiteSpace(input.SyncToken) ? input.SyncToken : "0" }
        };
        
        if(!string.IsNullOrWhiteSpace(input.ClassReferenceId))
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

        var invoiceWrapper = await Client.ExecuteWithJson<InvoiceWrapper>("/invoice", Method.Post, data, Creds);
        return new GetInvoiceResponse(invoiceWrapper.Invoice);
    }

    [Action("Delete invoice", Description = "Delete an invoice")]
    public async Task DeleteInvoice([ActionParameter] DeleteInvoiceRequest input)
    {
        var data = new Dictionary<string, object>
        {
            { "Id", input.InvoiceId },
            { "SyncToken", !string.IsNullOrWhiteSpace(input.SyncToken) ? input.SyncToken : "0" }
        };

        if(!string.IsNullOrWhiteSpace(input.ClassReferenceId))
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
        var data = new Dictionary<string, object>
        {
            { "Id", request.InvoiceId },
            { "SyncToken", !string.IsNullOrWhiteSpace(request.SyncToken) ? request.SyncToken : "0" }
        };

        if(!string.IsNullOrWhiteSpace(request.ClassReferenceId))
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
        
        var wrapper = await Client.ExecuteWithJson<InvoiceWrapper>($"/invoice/{request.InvoiceId}?operation=void", Method.Post, data, Creds);
        return new GetInvoiceResponse(wrapper.Invoice);
    }
}