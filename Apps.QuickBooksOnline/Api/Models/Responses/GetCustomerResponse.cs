using Apps.QuickBooksOnline.Dtos;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Api.Models.Responses;

public class GetCustomerResponse
{
    public GetCustomerResponse(CustomerDto customer)
    {
        Id = customer.Id;
        DisplayName = customer.DisplayName;
        SyncToken = customer.SyncToken;
    }

    [Display("Id")]
    public string Id { get; set; }

    [Display("Display name")]
    public string DisplayName { get; set; }

    [Display("Sync token")]
    public string SyncToken { get; set; }
}