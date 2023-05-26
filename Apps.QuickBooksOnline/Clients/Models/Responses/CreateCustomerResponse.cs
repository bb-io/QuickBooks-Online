using System.Text.Json.Serialization;

namespace Apps.QuickBooksOnline.Clients.Models.Responses
{
    public class CreateCustomerResponse
    {
        [JsonPropertyName("Id")]
        public string CustomerId { get; set; }
    }
}
