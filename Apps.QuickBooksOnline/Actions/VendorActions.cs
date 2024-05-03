using Apps.QuickBooksOnline.Models.Dtos.Vendors;
using Apps.QuickBooksOnline.Models.Requests.Vendors;
using Apps.QuickBooksOnline.Models.Responses.Vendors;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
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
    
    [Action("Get vendor", Description = "Get vendor by ID")]
    public async Task<VendorResponse> GetVendorById([ActionParameter] VendorRequest request)
    {
        var dto = await Client.ExecuteWithJson<GetVendorDto>($"/vendor/{request.VendorId}", Method.Get, null, Creds);
        return new VendorResponse(dto.Vendor);
    }
    
    [Action("Create vendor", Description = "Registers a new vendor with the provided details including name, contact information, and address.")]
    public async Task<VendorResponse> CreateVendor([ActionParameter] CreateVendorRequest request)
    {
        if (string.IsNullOrEmpty(request.DisplayName) &&
            string.IsNullOrEmpty(request.Title) &&
            string.IsNullOrEmpty(request.GivenName) &&
            string.IsNullOrEmpty(request.MiddleName) &&
            string.IsNullOrEmpty(request.FamilyName) &&
            string.IsNullOrEmpty(request.Suffix))
        {
            throw new Exception("Display name or at least one of the name components must be provided.");
        }

        var body = new
        {
            request.DisplayName,
            request.Title,
            request.GivenName,
            request.MiddleName,
            request.FamilyName,
            request.Suffix,
            PrimaryEmailAddr = request.PrimaryEmailAddr != null ? new { Address = request.PrimaryEmailAddr } : null,
            WebAddr = request.WebAddr != null ? new { URI = request.WebAddr } : null,
            PrimaryPhone = request.PrimaryPhone != null ? new { FreeFormNumber = request.PrimaryPhone } : null,
            Mobile = request.Mobile != null ? new { FreeFormNumber = request.Mobile } : null,
            request.TaxIdentifier,
            request.AcctNum,
            request.CompanyName,
            BillAddr = request.AddressLine1 != null ? new
            {
                Line1 = request.AddressLine1,
                Line2 = request.AddressLine2,
                Line3 = request.AddressLine3,
                City = request.City,
                PostalCode = request.PostalCode,
                Country = request.Country,
                CountrySubDivisionCode = request.StateCode
            } : null,
            request.PrintOnCheckName
        };

        var response = await Client.ExecuteWithJson<GetVendorDto>("/vendor", Method.Post, body, Creds);
        return new VendorResponse(response.Vendor);
    }
}