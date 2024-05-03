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
}