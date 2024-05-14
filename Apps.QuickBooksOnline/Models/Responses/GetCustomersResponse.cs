using Apps.QuickBooksOnline.Api.Models.Responses;

namespace Apps.QuickBooksOnline.Models.Responses;

public class GetCustomersResponse
{
    public List<GetCustomerResponse> Customers { get; set; } = new();
}