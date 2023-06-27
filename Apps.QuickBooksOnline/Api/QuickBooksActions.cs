using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common;
using RestSharp;
using Apps.QuickBooksOnline.Contracts;
using Apps.QuickBooksOnline.Clients;
using Apps.QuickBooksOnline.Clients.Models.Requests;
using Apps.QuickBooksOnline.Clients.Models.Responses;
using Apps.QuickBooksOnline.Constants;
using Apps.QuickBooksOnline.Exntensions;
using Line = Apps.QuickBooksOnline.Clients.Models.Requests.Line;
using SalesItemLineDetail = Apps.QuickBooksOnline.Clients.Models.Requests.SalesItemLineDetail;
using Item = Apps.QuickBooksOnline.Clients.Models.Requests.Item;
using Apps.QuickBooksOnline.Dtos;

namespace Apps.QuickBooksOnline.Api
{
    [ActionList]
    public class QuickBooksActions
    {
        [Action("Create a customer", Description = "Create a customer")]
        public GetCustomerResponse CreateCustomer(
           IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] CreateCustomerParameters input)
        {
            var client = new QuickBooksClient(authenticationCredentialsProviders);
            var request = new QuickBooksRequest("/customer", Method.Post, authenticationCredentialsProviders);
            request.AddQueryParameter("minorversion", authenticationCredentialsProviders.GetValueByName(AppConstants.MinorVersionName));
            request.AddJsonBody(new CreateCustomerRequest
            {
                DisplayName = input.DisplayName,
            });

            return new GetCustomerResponse(client.Post<CustomerWrapper>(request)?.Customer);
        }

        [Action("Update a customer", Description = "Update a customer")]
        public void UpdateCustomer(
           IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] UpdateCustomerParameters input)
        {
            var client = new QuickBooksClient(authenticationCredentialsProviders);
            var request = new QuickBooksRequest("/customer", Method.Post, authenticationCredentialsProviders);
            request.AddQueryParameter("minorversion", authenticationCredentialsProviders.GetValueByName(AppConstants.MinorVersionName));
            request.AddJsonBody(new UpdateCustomerRequest
            {
                DisplayName = input.DisplayName,
                CustomerId = input.CustomerId,
                SyncToken = input.SyncToken
            });

            client.Post(request);
        }

        [Action("Get a customer", Description = "Get a customer")]
        public GetCustomerResponse? GetCustomer(
          IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
          [ActionParameter] GetCustomerParameters input)
        {
            var client = new QuickBooksClient(authenticationCredentialsProviders);
            var request = new QuickBooksRequest($"/customer/{input.CustomerId}", Method.Get, authenticationCredentialsProviders);
            request.AddQueryParameter("minorversion", authenticationCredentialsProviders.GetValueByName(AppConstants.MinorVersionName));

            return new GetCustomerResponse(client.Get<CustomerWrapper>(request)?.Customer);
        }

        [Action("Create an invoice", Description = "Create an invoice")]
        public GetInvoiceResponse CreateInvoice(
           IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] CreateInvoiceParameters input)
        {
            var client = new QuickBooksClient(authenticationCredentialsProviders);
            var request = new QuickBooksRequest("/invoice", Method.Post, authenticationCredentialsProviders);
            request.AddQueryParameter("minorversion", authenticationCredentialsProviders.GetValueByName(AppConstants.MinorVersionName));
            request.AddJsonBody(CreateRequestBody<CreateInvoiceRequest, CreateInvoiceParameters>(input));

            return new GetInvoiceResponse(client.Post<InvoiceWrapper>(request)?.Invoice);
        }

        [Action("Update an invoice", Description = "Update an invoice")]
        public void UpdateInvoice(
           IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] UpdateInvoiceParameters input)
        {
            var client = new QuickBooksClient(authenticationCredentialsProviders);
            var request = new QuickBooksRequest("/invoice", Method.Post, authenticationCredentialsProviders);
            request.AddQueryParameter("minorversion", authenticationCredentialsProviders.GetValueByName(AppConstants.MinorVersionName));
            var requestBody = CreateRequestBody<UpdateInvoiceRequest, UpdateInvoiceParameters>(input);
            
            requestBody.InvoiceId = input.InvoiceId;
            requestBody.SyncToken = input.SyncToken;
            request.AddJsonBody(requestBody);

            client.Post(request);
        }

        [Action("Add line to invoice", Description = "Add line to invoice")]
        public void AddInvoiceLine(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] AddInvoiceLineParameters input)
        {
            var client = new QuickBooksClient(authenticationCredentialsProviders);
            var request = new QuickBooksRequest("/invoice", Method.Post, authenticationCredentialsProviders);
            request.AddQueryParameter("minorversion", authenticationCredentialsProviders.GetValueByName(AppConstants.MinorVersionName));
            var invoice = GetInvoiceOriginal(authenticationCredentialsProviders, new GetInvoiceParameters() { InvoiceId = input.InvoiceId });
            List<Dtos.Line> lines = invoice.Invoice.Line.ToList();
            lines.Add(new Dtos.Line
            {
                Amount = input.LineAmount,
                DetailType = "SalesItemLineDetail",
                SalesItemLineDetail = new Dtos.SalesItemLineDetail
                {
                    Item = new Dtos.Item
                    {
                        Name = input.ItemName,
                        Id = input.ItemId
                    }
                }
            });
            request.AddJsonBody(new AddInvoiceLineDto()
            {
                Id = input.InvoiceId,
                SyncToken = input.SyncToken,
                sparse = true,
                Line = lines
            });
            client.Post(request);
        }

        [Action("Delete an invoice", Description = "Delete an invoice")]
        public void DeleteInvoice(
           IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] DeleteInvoiceParameters input)
        {
            var client = new QuickBooksClient(authenticationCredentialsProviders);
            var request = new QuickBooksRequest("/invoice", Method.Post, authenticationCredentialsProviders);
            request.AddQueryParameter("operation", "delete");
            request.AddQueryParameter("minorversion", authenticationCredentialsProviders.GetValueByName(AppConstants.MinorVersionName));
            request.AddJsonBody(new DeleteInvoiceRequest
            {
                InvoiceId = input.InvoiceId,
                SyncToken = input.SyncToken
            });

            client.Post(request);
        }

        [Action("Get an invoice", Description = "Get an invoice")]
        public GetInvoiceResponse GetInvoice(
            IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] GetInvoiceParameters input)
        {
            return new GetInvoiceResponse(GetInvoiceOriginal(authenticationCredentialsProviders, input).Invoice);
        }

        private InvoiceWrapper GetInvoiceOriginal(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, 
            GetInvoiceParameters input)
        {
            var client = new QuickBooksClient(authenticationCredentialsProviders);
            var request = new QuickBooksRequest($"/invoice/{input.InvoiceId}", Method.Get, authenticationCredentialsProviders);
            request.AddQueryParameter("minorversion", authenticationCredentialsProviders.GetValueByName(AppConstants.MinorVersionName));
            return client.Get<InvoiceWrapper>(request);
        }

        private TRequest CreateRequestBody<TRequest, TInput>(TInput input)
            where TRequest : CreateInvoiceRequest, new()
            where TInput : CreateInvoiceParameters
        {
            return new TRequest
            {
                Customer = new Clients.Models.Requests.Customer
                {
                    Id = input.CustomerId
                },
                Line = new Line[]
                 {
                     new Line
                     {
                         Amount = input.LineAmount,
                         DetailType = "SalesItemLineDetail",
                         SalesItemLineDetail = new SalesItemLineDetail
                         {
                              Item = new Item
                              {
                                   Name = input.ItemName,
                                   Id = input.ItemId
                              }
                         }
                     }
                 }
            };
        }


    }
}
