﻿using Apps.QuickBooksOnline.Api.Models.Requests;
using Apps.QuickBooksOnline.Api.Models.Responses;
using Apps.QuickBooksOnline.Models.Dtos.Payments;
using Apps.QuickBooksOnline.Models.Dtos.Vendors;
using Apps.QuickBooksOnline.Models.Requests.Vendors;
using Apps.QuickBooksOnline.Models.Responses.Vendors;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.QuickBooksOnline.Actions;

[ActionList]
public class VendorActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Get all vendors", Description = "Get all vendors")]
    public async Task<GetAllVendorsResponse> GetAllVendors()
    {
        var sql = "select * from Vendor";
        var vendorsWrapper =
            await Client.ExecuteWithJson<QueryVendorsWrapper>($"/query?query={sql}", Method.Get, null, Creds);

        return new GetAllVendorsResponse(vendorsWrapper.QueryResponse.Vendor);
    }

    [Action("Find vendor", Description = "Returns the first matching vendor given the provided criteria")]
    public async Task<GetVendorResponse> FindVendor([ActionParameter] GetVendorFilterRequest request)
    {
        var lastUpdatedTime = request.LastUpdatedTime?.ToString("yyyy-MM-dd") ?? "2015-03-01";
        var sql = $"select * from Vendor Where MetaData.LastUpdatedTime > '{lastUpdatedTime}'";

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

        if (!string.IsNullOrEmpty(request.AcctNum))
        {
            sql += $" AND AcctNum = '{request.AcctNum}'";
        }

        var vendorsWrapper = await Client.ExecuteWithJson<QueryVendorsWrapper>($"/query?query={sql}", Method.Get, null, Creds);

        if (vendorsWrapper.QueryResponse.Vendor == null || vendorsWrapper.QueryResponse.Vendor.Count == 0)
        {
            return null;
        }

        return new GetVendorResponse(vendorsWrapper.QueryResponse.Vendor.First());
    }


    [Action("Get vendor", Description = "Get vendor by ID")]
    public async Task<VendorResponse> GetVendorById([ActionParameter] VendorRequest request)
    {
        var dto = await Client.ExecuteWithJson<GetVendorDto>($"/vendor/{request.VendorId}", Method.Get, null, Creds);
        return new VendorResponse(dto.Vendor);
    }

    [Action("Update vendor",
        Description =
            "Updates an existing vendor with provided details.")]
    public async Task<VendorResponse> UpdateVendor([ActionParameter] UpdateVendorRequest request)
    {
        var dto = await Client.ExecuteWithJson<GetVendorDto>($"/vendor/{request.VendorId}", Method.Get, null, Creds);

        if (request.DisplayName != null)
        {
            dto.Vendor.DisplayName = request.DisplayName;
        }

        if (request.Title != null)
        {
            dto.Vendor.Title = request.Title;
        }

        if (request.GivenName != null)
        {
            dto.Vendor.GivenName = request.GivenName;
        }

        if (request.FamilyName != null)
        {
            dto.Vendor.FamilyName = request.FamilyName;
        }

        if (request.Suffix != null)
        {
            dto.Vendor.Suffix = request.Suffix;
        }

        if (request.PrimaryEmailAddr != null)
        {
            dto.Vendor.PrimaryEmailAddr = new PrimaryEmailAddress { Address = request.PrimaryEmailAddr };
        }

        if (request.WebAddr != null)
        {
            dto.Vendor.WebAddr = new WebAddrDto { URI = request.WebAddr };
        }

        if (request.PrimaryPhone != null)
        {
            dto.Vendor.PrimaryPhone = new PhoneDto { FreeFormNumber = request.PrimaryPhone };
        }

        if (request.Mobile != null)
        {
            dto.Vendor.Mobile = new PhoneDto { FreeFormNumber = request.Mobile };
        }

        if (request.TaxIdentifier != null)
        {
            dto.Vendor.TaxIdentifier = request.TaxIdentifier;
        }

        if (request.AcctNum != null)
        {
            dto.Vendor.AcctNum = request.AcctNum;
        }

        if (request.CompanyName != null)
        {
            dto.Vendor.CompanyName = request.CompanyName;
        }

        if (request.PrintOnCheckName != null)
        {
            dto.Vendor.PrintOnCheckName = request.PrintOnCheckName;
        }

        if (request.AddressLine1 != null)
        {
            dto.Vendor.BillAddr.Line1 = request.AddressLine1;
        }

        if (request.City != null)
        {
            dto.Vendor.BillAddr.City = request.City;
        }

        if (request.PostalCode != null)
        {
            dto.Vendor.BillAddr.PostalCode = request.PostalCode;
        }

        if (request.Country != null)
        {
            dto.Vendor.Country = request.Country;
        }

        if (request.StateCode != null)
        {
            dto.Vendor.BillAddr.CountrySubDivisionCode = request.StateCode;
        }

        var response =
            await Client.ExecuteWithJson<GetVendorDto>("/vendor", Method.Post, dto, Creds);

        return new VendorResponse(response.Vendor);
    }

    [Action("Create vendor",
        Description =
            "Registers a new vendor with provided details.")]
    public async Task<VendorResponse> CreateVendor([ActionParameter] CreateVendorRequest request)
    {
        if (string.IsNullOrEmpty(request.DisplayName))
        {
            throw new PluginMisconfigurationException("Display name must be provided.");
        }

        var body = new Dictionary<string, object>
        {
            { "DisplayName", request.DisplayName }
        };


        AddPropertyIfNotNull(body, "CurrencyRef", request.CurrencyCode != null ? new { value = request.CurrencyCode }: null);
        AddPropertyIfNotNull(body, "Title", request.Title);
        AddPropertyIfNotNull(body, "GivenName", request.GivenName);
        AddPropertyIfNotNull(body, "MiddleName", request.MiddleName);
        AddPropertyIfNotNull(body, "FamilyName", request.FamilyName);
        AddPropertyIfNotNull(body, "Suffix", request.Suffix);
        AddPropertyIfNotNull(body, "PrimaryEmailAddr",
            request.PrimaryEmailAddr != null ? new { Address = request.PrimaryEmailAddr } : null);
        AddPropertyIfNotNull(body, "WebAddr", request.WebAddr != null ? new { URI = request.WebAddr } : null);
        AddPropertyIfNotNull(body, "PrimaryPhone",
            request.PrimaryPhone != null ? new { FreeFormNumber = request.PrimaryPhone } : null);
        AddPropertyIfNotNull(body, "Mobile", request.Mobile != null ? new { FreeFormNumber = request.Mobile } : null);
        AddPropertyIfNotNull(body, "TaxIdentifier", request.TaxIdentifier);
        AddPropertyIfNotNull(body, "AcctNum", request.AcctNum);
        AddPropertyIfNotNull(body, "CompanyName", request.CompanyName);
        AddPropertyIfNotNull(body, "PrintOnCheckName", request.PrintOnCheckName);

        if (request.AddressLine1 != null || request.AddressLine2 != null || request.AddressLine3 != null ||
            request.City != null || request.PostalCode != null || request.Country != null || request.StateCode != null)
        {
            var address = new Dictionary<string, object>();
            AddPropertyIfNotNull(address, "Line1", request.AddressLine1);
            AddPropertyIfNotNull(address, "Line2", request.AddressLine2);
            AddPropertyIfNotNull(address, "Line3", request.AddressLine3);
            AddPropertyIfNotNull(address, "City", request.City);
            AddPropertyIfNotNull(address, "PostalCode", request.PostalCode);
            AddPropertyIfNotNull(address, "Country", request.Country);
            AddPropertyIfNotNull(address, "CountrySubDivisionCode", request.StateCode);

            body.Add("BillAddr", address);
        }

        var response =
            await Client.ExecuteWithJson<GetVendorDto>("/vendor", Method.Post, body, Creds);
        return new VendorResponse(response.Vendor);
    }

    private static void AddPropertyIfNotNull(Dictionary<string, object> dict, string key, object? value)
    {
        if (value != null)
        {
            dict.Add(key, value);
        }
    }
}