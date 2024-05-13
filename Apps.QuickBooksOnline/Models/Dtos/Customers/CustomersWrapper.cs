using Apps.QuickBooksOnline.Dtos;
using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Customers;

public class QueryCustomerWrapper
{
    [JsonProperty("QueryResponse")]
    public CustomersWrapper QueryResponse { get; set; }
}

public class CustomersWrapper
{
    [JsonProperty("Customer")]
    public List<CustomerDto> Customer { get; set; }
}