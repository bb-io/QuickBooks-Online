using Apps.QuickBooksOnline.Api.Models.Responses;
using Apps.QuickBooksOnline.Clients.Models.Requests;
using Apps.QuickBooksOnline.Contracts;
using Apps.QuickBooksOnline.Dtos;
using Apps.QuickBooksOnline.Models.Dtos.Customers;
using Apps.QuickBooksOnline.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using UpdateCustomerRequest = Apps.QuickBooksOnline.Contracts.UpdateCustomerRequest;

namespace Apps.QuickBooksOnline.Actions;

[ActionList]
public class CustomerActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Get all customers", Description = "Get all customers")]
    public async Task<GetCustomersResponse> GetAllCustomers()
    {
        string sql = "select * from Customer Where Metadata.LastUpdatedTime > '2015-03-01'";
        var wrapper = await Client.ExecuteWithJson<QueryCustomerWrapper>($"/query?query={sql}", Method.Get, null, Creds);
        return new GetCustomersResponse
        {
            Customers = wrapper.QueryResponse.Customer.Select(c => new GetCustomerResponse(c)).ToList()
        };
    }
    
    [Action("Get customer", Description = "Get a customer")]
    public async Task<GetCustomerResponse> GetCustomer([ActionParameter] CustomerRequest input)
    {
        var wrapper = await Client.ExecuteWithJson<CustomerWrapper>($"/customer/{input.CustomerId}", Method.Get, null, Creds);
        return new GetCustomerResponse(wrapper.Customer);
    }
    
    [Action("Create customer", Description = "Create a customer")]
    public async Task<GetCustomerResponse> CreateCustomer([ActionParameter] CreateCustomerParameters input)
    {
        var body = new CreateCustomerRequest
        {
            DisplayName = input.MiddleName,
        };
        
        var response = await Client.ExecuteWithJson<CustomerWrapper>("/customer", Method.Post, body, Creds);
        return new GetCustomerResponse(response.Customer);
    }

    [Action("Update customer", Description = "Update a customer")]
    public async Task<GetCustomerResponse> UpdateCustomer([ActionParameter] UpdateCustomerRequest input)
    {
        var body = new Clients.Models.Requests.UpdateCustomerRequest
        {
            DisplayName = input.MiddleName,
            CustomerId = input.CustomerId,
            SyncToken = input.SyncToken ?? "0"
        };
        
        var wrapper = await Client.ExecuteWithJson<CustomerWrapper>("/customer", Method.Post, body, Creds);
        return new GetCustomerResponse(wrapper.Customer);
    }
}