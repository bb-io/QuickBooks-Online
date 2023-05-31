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

namespace Apps.QuickBooksOnline.Api
{
    [ActionList]
    public class QuickBooksActions
    {
        [Action("Create a customer", Description = "Create a customer")]
        public Clients.Models.Responses.Customer? CreateCustomer(
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

            return client.Post<CreateCustomerResponse>(request)?.Customer;
        }

        [Action("Create an invoice", Description = "Create an invoice")]
        public void CreateInvoice(
           IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] CreateInvoiceParameters input)
        {
            var client = new QuickBooksClient(authenticationCredentialsProviders);
            var request = new QuickBooksRequest("/invoice", Method.Post, authenticationCredentialsProviders);
            request.AddQueryParameter("minorversion", authenticationCredentialsProviders.GetValueByName(AppConstants.MinorVersionName));
            request.AddJsonBody(CreateRequestBody(input));

            client.Post(request);
        }

        private CreateInvoiceRequest CreateRequestBody(CreateInvoiceParameters input)
        {
            return new CreateInvoiceRequest
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
