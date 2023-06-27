using System.Text.Json.Serialization;

namespace Apps.QuickBooksOnline.Clients.Models.Responses
{
    public class GetCustomerResponse
    {
        [JsonPropertyName("Customer")]
        public Customer Customer { get; set; }
    }
}
