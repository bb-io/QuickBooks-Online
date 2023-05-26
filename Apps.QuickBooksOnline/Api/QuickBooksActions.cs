using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common;
using RestSharp;
using Apps.QuickBooksOnline.Contracts;
using Apps.QuickBooksOnline.Clients;
using Apps.QuickBooksOnline.Clients.Models.Requests;
using Apps.QuickBooksOnline.Clients.Models.Responses;

namespace Apps.QuickBooksOnline.Api
{
    [ActionList]
    public class QuickBooksActions
    {
        [Action("Create a customer", Description = "Create a customer")]
        public void CreateCustomer(
           IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] CreateCustomerParameters input)
        {
            var client = new QuickBooksClient();
            var request = new QuickBooksRequest($"customer?minorversion=65", Method.Post, authenticationCredentialsProviders);
            request.AddJsonBody(new CreateCustomerRequest
            {
                DisplayName = input.DisplayName,
                FamilyName = input.FamilyName,
                GivenName = input.GivenName,
                MiddleName = input.MiddleName,
                Suffix = input.Suffix,
                Title = input.Title
            });

            client.Execute(request);
        }

        [Action("Create an invoice", Description = "Create an invoice")]
        public CreateCustomerResponse? CreateInvoice(
           IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] CreateInvoiceParameters input)
        {
            var client = new QuickBooksClient();
            var request = new QuickBooksRequest($"/invoice?minorversion=65", Method.Post, authenticationCredentialsProviders);
            request.AddJsonBody(CreateRequestBody(input));

            return client.Post<CreateCustomerResponse>(request);
        }

        private CreateInvoiceRequest CreateRequestBody(CreateInvoiceParameters input)
        {
            return new CreateInvoiceRequest
            {
                Customer = new Customer
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
                                   Value = input.ItemId
                              }
                         }
                     }
                 }
            };
        }
    }
}
