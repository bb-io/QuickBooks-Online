﻿using RestSharp;
using Apps.QuickBooksOnline.Api.Models.Responses;
using Apps.QuickBooksOnline.Contracts;
using Apps.QuickBooksOnline.Models.Dtos;
using Apps.QuickBooksOnline.Models.Dtos.Invoices;
using Apps.QuickBooksOnline.Models.Requests.Invoices;
using Apps.QuickBooksOnline.Models.Responses.Invoices;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.Actions;

[ActionList]
public class InvoiceActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Get all invoices", Description = "Get all invoices")]
    public async Task<GetAllInvoicesResponse> GetAllInvoices()
    {
        var invoicesWrapper = await Client.ExecuteWithJson<InvoicesWrapper>("/invoice", Method.Get, null, Creds);
        return new GetAllInvoicesResponse(invoicesWrapper.Invoice);
    }
    
    [Action("Get invoice", Description = "Get an invoice")]
    public async Task<GetInvoiceResponse> GetInvoice([ActionParameter] InvoiceRequest input)
    {
        var invoiceWrapper = await Client.ExecuteWithJson<InvoiceWrapper>($"/invoice/{input.InvoiceId}", Method.Get, null, Creds);
        return new GetInvoiceResponse(invoiceWrapper.Invoice);
    }
    
    [Action("Create invoice", Description = "Create an invoice")]
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

    [Action("Update invoice", Description = "Sparse update an invoice")]
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
    
    [Action("Delete invoice", Description = "Delete an invoice")]
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