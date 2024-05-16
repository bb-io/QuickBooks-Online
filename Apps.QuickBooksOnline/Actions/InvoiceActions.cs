using System.Globalization;
using Apps.QuickBooksOnline.Api.Models.Requests;
using RestSharp;
using Apps.QuickBooksOnline.Api.Models.Responses;
using Apps.QuickBooksOnline.Contracts;
using Apps.QuickBooksOnline.Models.Dtos;
using Apps.QuickBooksOnline.Models.Dtos.Invoices;
using Apps.QuickBooksOnline.Models.Dtos.Invoices.Common;
using Apps.QuickBooksOnline.Models.Requests.Invoices;
using Apps.QuickBooksOnline.Models.Responses.Invoices;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Newtonsoft.Json;
using CustomerRef = Apps.QuickBooksOnline.Models.Dtos.CustomerRef;
using ItemRef = Apps.QuickBooksOnline.Models.Dtos.ItemRef;

namespace Apps.QuickBooksOnline.Actions;

[ActionList]
public class InvoiceActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    [Action("Get all invoices", Description = "Get all invoices")]
    public async Task<GetAllInvoicesResponse> GetAllInvoices([ActionParameter] InvoiceFilterRequest request)
    {
        var sql = BuildInvoiceQuery(request);
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

        var lines = new List<SalesLine>();
        if (input.ItemIds is null)
        {
            lines = input.LineAmounts.Select((t, i) => new SalesLine
            {
                DetailType = "SalesItemLineDetail",
                Amount = (decimal)t,
                SalesItemLineDetail = new Api.Models.Requests.SalesItemLineDetail
                {
                    Qty = input.Quantities?.ElementAt(i) == null
                        ? 1
                        : input.Quantities.ElementAt(i),
                    UnitPrice = input.UnitPrices?.ElementAt(i) == null ||
                                !decimal.TryParse(input.UnitPrices.ElementAt(i), out var price)
                        ? null
                        : price,
                }
            }).ToList();
        }
        else
        {
            var items = await itemActions.GetItemsByIds(input.ItemIds);
            lines = items.Select((t, i) => new SalesLine
            {
                DetailType = "SalesItemLineDetail",
                Amount = (decimal)input.LineAmounts.ElementAt(i),
                SalesItemLineDetail = new Api.Models.Requests.SalesItemLineDetail
                {
                    ItemRef = new ItemRef
                    {
                        Name = t.Name,
                        Value = t.Id,
                        ClassRef = input.ClassIds?.ElementAt(i) == null
                            ? null
                            : new ClassRef
                            {
                                Value = input.ClassIds?.ElementAt(i)
                            }
                    },
                    Qty = input.Quantities?.ElementAt(i) == null
                        ? 1
                        : input.Quantities.ElementAt(i),
                    UnitPrice = input.UnitPrices?.ElementAt(i) == null ||
                                !decimal.TryParse(input.UnitPrices.ElementAt(i), out var price)
                        ? null
                        : price,
                }
            }).ToList();
        }

        var body = new CreateInvoiceRequestBody
        {
            Line = lines,
            CustomerRef = new CustomerRef
            {
                Value = input.CustomerId
            },
            TxnDate = input.InvoiceDate?.ToString("yyyy-MM-dd"),
            DocNumber = input.DocNumber,
            ClassRef = new ClassRef
            {
                Value = input.ClassId
            }
        };

        var invoiceWrapper = await Client.ExecuteWithJson<InvoiceWrapper>("/invoice", Method.Post, body, Creds);
        return new GetInvoiceResponse(invoiceWrapper.Invoice);
    }

    [Action("Update invoice", Description = "Update an invoice with a new due date and class reference")]
    public async Task<GetInvoiceResponse> UpdateInvoice([ActionParameter] UpdateInvoiceRequest input)
    {
        var syncToken = await GetSyncTokenAsync(input.InvoiceId, input.SyncToken);
        var updatedInvoice = new UpdateInvoiceRequestBody
        {
            Id = input.InvoiceId,
            SyncToken = syncToken
        };

        if (input.ItemIds is null)
        {
            updatedInvoice.Line = input.LineAmounts.Select((t, i) => new SalesLine
            {
                DetailType = "SalesItemLineDetail",
                Amount = (decimal)t,
                SalesItemLineDetail = new Api.Models.Requests.SalesItemLineDetail
                {
                    Qty = input.Quantities?.ElementAt(i) == null
                        ? 1
                        : input.Quantities.ElementAt(i),
                    UnitPrice = input.UnitPrices?.ElementAt(i) == null ||
                                !decimal.TryParse(input.UnitPrices.ElementAt(i), out var price)
                        ? null
                        : price,
                }
            }).ToList();
        }
        else
        {
            var itemActions = new ItemActions(InvocationContext);
            var items = await itemActions.GetItemsByIds(input.ItemIds);
            updatedInvoice.Line = items.Select((t, i) => new SalesLine
            {
                DetailType = "SalesItemLineDetail",
                Amount = (decimal)input.LineAmounts.ElementAt(i),
                SalesItemLineDetail = new Api.Models.Requests.SalesItemLineDetail
                {
                    ItemRef = new ItemRef
                    {
                        Name = t.Name,
                        Value = t.Id,
                        ClassRef = input.ClassIds?.ElementAt(i) == null
                            ? null
                            : new ClassRef
                            {
                                Value = input.ClassIds?.ElementAt(i)
                            }
                    },
                    Qty = input.Quantities?.ElementAt(i) == null
                        ? 1
                        : input.Quantities.ElementAt(i),
                    UnitPrice = input.UnitPrices?.ElementAt(i) == null ||
                                !decimal.TryParse(input.UnitPrices.ElementAt(i), out var price)
                        ? null
                        : price,
                }
            }).ToList();
        }

        if (!string.IsNullOrEmpty(input.CustomerId))
        {
            updatedInvoice.CustomerRef = new CustomerRef
            {
                Value = input.CustomerId
            };
        }
        
        if (!string.IsNullOrEmpty(input.ClassId))
        {
            updatedInvoice.ClassRef = new ClassRef
            {
                Value = input.ClassId
            };
        }

        updatedInvoice.TxnDate = input.InvoiceDate?.ToString("yyyy-MM-dd");
        updatedInvoice.DueDate = input.DueDate?.ToString("yyyy-MM-dd");

        var invoiceWrapper = await Client.ExecuteWithJson<InvoiceWrapper>($"/invoice",
            Method.Post, updatedInvoice, Creds);
        return new GetInvoiceResponse(invoiceWrapper.Invoice);
    }

    [Action("Import invoice", Description = "Import invoice from JSON")]
    public async Task<GetInvoiceResponse> ImportInvoice([ActionParameter] ImportInvoiceRequest request)
    {
        var stream = await fileManagementClient.DownloadAsync(request.File);

        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();

        var invoicesObject = JsonConvert.DeserializeObject<InvoicesObject>(json);
        var invoiceResponses = new List<GetInvoiceResponse>();

        foreach (var invoice in invoicesObject?.Invoices!)
        {
            var invoiceRequest = new CreateInvoiceRequest
            {
                CustomerId = request.CustomerId,
                LineAmounts = invoice.Lines.Select(x => (double)x.Amount).ToList(),
                Quantities = invoice.Lines.Select(x => x.Quantity).ToList(),
                UnitPrices = invoice.Lines.Select(x => x.UnitPrice.ToString(CultureInfo.InvariantCulture)).ToList(),
                InvoiceDate = invoice.InvoiceDate,
                Descriptions = invoice.Lines.Select(x => x.Description).ToList(),
                DocNumber = invoice.InvoiceNumber,
                ItemIds = null,
                ClassIds = null
            };

            var invoiceResponse = string.IsNullOrEmpty(request.InvoiceId)
                ? await CreateInvoice(invoiceRequest)
                : await UpdateInvoice(new UpdateInvoiceRequest(invoiceRequest) { InvoiceId = request.InvoiceId });

            invoiceResponses.Add(invoiceResponse);
        }

        return invoiceResponses.First();
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

    [Action("Get invoice custom field", Description = "Get a custom field value from an invoice")]
    public async Task<string> GetCustomField([ActionParameter] GetCustomFieldRequest request)
    {
        var invoiceWrapper =
            await Client.ExecuteWithJson<InvoiceWrapper>($"/invoice/{request.InvoiceId}", Method.Get, null, Creds);

        return invoiceWrapper.Invoice.CustomField
            .FirstOrDefault(x => x.DefinitionId == request.CustomFieldId)?.StringValue ?? string.Empty;
    }

    [Action("Set invoice custom field", Description = "Set a custom field value for an invoice")]
    public async Task SetCustomField([ActionParameter] SetCustomFieldRequest request)
    {
        var invoiceWrapper =
            await Client.ExecuteWithJson<InvoiceWrapper>($"/invoice/{request.InvoiceId}", Method.Get, null, Creds);

        var customField = invoiceWrapper.Invoice.CustomField
            .FirstOrDefault(x => x.DefinitionId == request.CustomFieldId);

        if (customField is null)
        {
            throw new InvalidOperationException("Custom field not found");
        }

        customField.StringValue = request.CustomFieldValue;
        invoiceWrapper.Invoice.Sparse = true;

        await Client.ExecuteWithJson<InvoiceWrapper>($"/invoice", Method.Post, invoiceWrapper.Invoice, Creds);
    }

    private async Task<string> GetSyncTokenAsync(string invoiceId, string? syncToken)
    {
        if (string.IsNullOrWhiteSpace(syncToken))
        {
            return (await GetInvoice(new InvoiceRequest { InvoiceId = invoiceId })).SyncToken;
        }

        return syncToken;
    }

    private string BuildInvoiceQuery(InvoiceFilterRequest request)
    {
        var sql = "SELECT * FROM Invoice";
        
        var conditions = new List<string>();
        if (request.Paid.HasValue)
        {
            conditions.Add(request.Paid.Value ? "Balance = '0'" : "Balance > '0'");
        }

        if (request.Overdue.HasValue)
        {
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            conditions.Add(request.Overdue.Value ? $"DueDate < '{currentDate}'" : $"DueDate >= '{currentDate}'");
        }

        if (conditions.Count > 0)
        {
            sql += " WHERE " + string.Join(" AND ", conditions);
        }
        
        return sql;
    }
}