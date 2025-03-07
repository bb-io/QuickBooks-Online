﻿using Apps.QuickBooksOnline.Api.Models.Responses;
using Apps.QuickBooksOnline.Contracts;
using Apps.QuickBooksOnline.Dtos;
using Apps.QuickBooksOnline.Models.Dtos.Customers;
using Apps.QuickBooksOnline.Models.Requests.Customers;
using Apps.QuickBooksOnline.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using CreateCustomerRequest = Apps.QuickBooksOnline.Models.Requests.Customers.CreateCustomerRequest;
using UpdateCustomerRequest = Apps.QuickBooksOnline.Models.Requests.Customers.UpdateCustomerRequest;

namespace Apps.QuickBooksOnline.Actions;

[ActionList]
public class CustomerActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Get all customers", Description = "Get all customers")]
    public async Task<GetCustomersResponse> GetAllCustomers([ActionParameter] GetCustomerFilterRequest request)
    {
        var lastUpdatedTime = request.LastUpdatedTime?.ToString("yyyy-MM-dd") ?? "2015-03-01";
        var sql = $"select * from Customer Where Metadata.LastUpdatedTime > '{lastUpdatedTime}'";

        if (!string.IsNullOrEmpty(request.DisplayName))
        {
            sql += $" AND DisplayName = '{request.DisplayName}'";
        }

        if (!string.IsNullOrEmpty(request.GivenName))
        {
            sql += $" AND GivenName = '{request.GivenName}'";
        }

        var wrapper =
            await Client.ExecuteWithJson<QueryCustomerWrapper>($"/query?query={sql}", Method.Get, null, Creds);
        
        if(wrapper.QueryResponse.Customer == null || wrapper.QueryResponse.Customer.Count == 0)
        {
            return new GetCustomersResponse();
        }
        
        return new GetCustomersResponse
        {
            Customers = wrapper.QueryResponse.Customer.Select(c => new GetCustomerResponse(c)).ToList()
        };
    }

    [Action("Find customer", Description = "Returns the first matching customer given the provided criteria")]
    public async Task<GetCustomerResponse> FindCustomer([ActionParameter] GetCustomerFilterRequest request)
    {
        var lastUpdatedTime = request.LastUpdatedTime?.ToString("yyyy-MM-dd") ?? "2015-03-01";
        var sql = $"select * from Customer Where Metadata.LastUpdatedTime > '{lastUpdatedTime}'";

        if (!string.IsNullOrEmpty(request.DisplayName))
        {
            sql += $" AND DisplayName = '{request.DisplayName}'";
        }

        if (!string.IsNullOrEmpty(request.GivenName))
        {
            sql += $" AND GivenName = '{request.GivenName}'";
        }

        if (!string.IsNullOrEmpty(request.CompanyName))
        {
            sql += $" AND CompanyName = '{request.CompanyName}'";
        }
        
        var wrapper =
            await Client.ExecuteWithJson<QueryCustomerWrapper>($"/query?query={sql}", Method.Get, null, Creds);

        if (wrapper.QueryResponse.Customer == null || wrapper.QueryResponse.Customer.Count == 0)
        {
            return null;
        }

         return new GetCustomerResponse(wrapper.QueryResponse.Customer.First());
        
    }

    [Action("Get customer", Description = "Get a customer")]
    public async Task<GetCustomerResponse> GetCustomer([ActionParameter] CustomerRequest input)
    {
        var wrapper =
            await Client.ExecuteWithJson<CustomerWrapper>($"/customer/{input.CustomerId}", Method.Get, null, Creds);
        return new GetCustomerResponse(wrapper.Customer);
    }

    [Action("Create customer", Description = "Create a customer")]
    public async Task<GetCustomerResponse> CreateCustomer([ActionParameter] CreateCustomerRequest input)
    {
        var body = new Dictionary<string, object>();

        if (!string.IsNullOrEmpty(input.DisplayName))
        {
            body.Add("DisplayName", input.DisplayName);
        }

        if (!string.IsNullOrEmpty(input.CompanyName))
        {
            body.Add("CompanyName", input.CompanyName);
        }

        if (!string.IsNullOrEmpty(input.GivenName))
        {
            body.Add("GivenName", input.GivenName);
        }
        
        if (!string.IsNullOrEmpty(input.MiddleName))
        {
            body.Add("MiddleName", input.MiddleName);
        }
        
        if (!string.IsNullOrEmpty(input.FamilyName))
        {
            body.Add("FamilyName", input.FamilyName);
        }
        
        if (!string.IsNullOrEmpty(input.PrimaryEmailAddress))
        {
            body.Add("PrimaryEmailAddr", new
            {
                Address = input.PrimaryEmailAddress
            });
        }
        
        if (!string.IsNullOrEmpty(input.PrimaryPhone))
        {
            body.Add("PrimaryPhone", new
            {
                FreeFormNumber = input.PrimaryPhone
            });
        }
        
        if (!string.IsNullOrEmpty(input.FullyQualifiedName))
        {
            body.Add("FullyQualifiedName", input.FullyQualifiedName);
        }
        
        if (!string.IsNullOrEmpty(input.Notes))
        {
            body.Add("Notes", input.Notes);
        }
        
        if (!string.IsNullOrEmpty(input.Suffix))
        {
            body.Add("Suffix", input.Suffix);
        }

        var billingAddress = new Dictionary<string, string>();

        if (!string.IsNullOrEmpty(input.CountrySubDivisionCode))
        {
            billingAddress.Add("CountrySubDivisionCode", input.CountrySubDivisionCode);
        }

        if (!string.IsNullOrEmpty(input.Line1))
        {
            billingAddress.Add("Line1", input.Line1);
        }

        if (!string.IsNullOrEmpty(input.Line2))
        {
            billingAddress.Add("Line2", input.Line2);
        }

        if (!string.IsNullOrEmpty(input.City))
        {
            billingAddress.Add("City", input.City);
        }

        if (!string.IsNullOrEmpty(input.PostalCode))
        {
            billingAddress.Add("PostalCode", input.PostalCode);
        }

        if (!string.IsNullOrEmpty(input.Country))
        {
            billingAddress.Add("Country", input.Country);
        }

        if (billingAddress.Count > 0)
        {
            body.Add("BillAddr", billingAddress);
        }

        if (!string.IsNullOrEmpty(input.Term))
        {
            if (int.TryParse(input.Term, out int value))
            {
                body.Add("SalesTermRef", new { value = input.Term });
            } else
            {
                throw new PluginMisconfigurationException("The Term ID value should be numeric and corresponding to the Term IDs in your Quickbooks connection.");
            }                
        }

        if (!string.IsNullOrEmpty(input.Currency))
        {
            body.Add("CurrencyRef", new { value = input.Currency });
        }
        try 
        {
            var response = await Client.ExecuteWithJson<CustomerWrapper>("/customer", Method.Post, body, Creds);
            return new GetCustomerResponse(response.Customer);
        }
        catch (Exception e)
        {
            if (e.Message.Contains("Duplicate Name Exists")) 
            {
                throw new PluginMisconfigurationException("A customer with the same name already exists");
            }
            else { throw new PluginApplicationException(e.Message); }
        }
        
    }

    [Action("Update customer", Description = "Update a customer with the given ID including optional fields")]
    public async Task<GetCustomerResponse> UpdateCustomer([ActionParameter] UpdateCustomerRequest request)
    {
        var syncToken = request.SyncToken ??
                        (await GetCustomer(new CustomerRequest { CustomerId = request.CustomerId })).SyncToken;
        var body = new Dictionary<string, object>()
        {
            { "Id", request.CustomerId },
            { "sparse", true },
            { "SyncToken", syncToken }
        };

        if (!string.IsNullOrEmpty(request.DisplayName))
        {
            body.Add("DisplayName", request.DisplayName);
        }

        if (!string.IsNullOrEmpty(request.GivenName))
        {
            body.Add("GivenName", request.GivenName);
        }
        
        if (!string.IsNullOrEmpty(request.MiddleName))
        {
            body.Add("MiddleName", request.MiddleName);
        }
        
        if (!string.IsNullOrEmpty(request.FamilyName))
        {
            body.Add("FamilyName", request.FamilyName);
        }
        
        if (!string.IsNullOrEmpty(request.PrimaryEmailAddress))
        {
            body.Add("PrimaryEmailAddr", new
            {
                Address = request.PrimaryEmailAddress
            });
        }
        
        if (!string.IsNullOrEmpty(request.PrimaryPhone))
        {
            body.Add("PrimaryPhone", new
            {
                FreeFormNumber = request.PrimaryPhone
            });
        }
        
        if (!string.IsNullOrEmpty(request.FullyQualifiedName))
        {
            body.Add("FullyQualifiedName", request.FullyQualifiedName);
        }
        
        if (!string.IsNullOrEmpty(request.Notes))
        {
            body.Add("Notes", request.Notes);
        }
        
        if (!string.IsNullOrEmpty(request.Suffix))
        {
            body.Add("Suffix", request.Suffix);
        }
        
        if (!string.IsNullOrEmpty(request.CountrySubDivisionCode))
        {
            body.Add("BillAddr", new
            {
                CountrySubDivisionCode = request.CountrySubDivisionCode,
                City = request.City,
                PostalCode = request.PostalCode,
                Line1 = request.Line1,
                Country = request.Country
            });
        }
        
        if(request.Job.HasValue)
        {
            body.Add("Job", request.Job);
        }
        
        if (!string.IsNullOrEmpty(request.Domain))
        {
            body.Add("Domain", request.Domain);
        }
        
        if(request.Active.HasValue)
        {
            body.Add("Active", request.Active);
        }

        var wrapper = await Client.ExecuteWithJson<CustomerWrapper>("/customer", Method.Post, body, Creds);
        return new GetCustomerResponse(wrapper.Customer);
    }
}