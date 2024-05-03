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

    [Action("Create vendor",
        Description =
            "Registers a new vendor with provided details, excluding any null fields to avoid validation errors.")]
    public async Task<VendorResponse> CreateVendor([ActionParameter] CreateVendorRequest request)
    {
        if (string.IsNullOrEmpty(request.DisplayName))
        {
            throw new Exception("Display name must be provided.");
        }

        var body = new Dictionary<string, object>
        {
            { "DisplayName", request.DisplayName }
        };

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