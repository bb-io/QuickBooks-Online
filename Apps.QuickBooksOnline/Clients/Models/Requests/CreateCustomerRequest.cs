using System.Text.Json.Serialization;

namespace Apps.QuickBooksOnline.Clients.Models.Requests
{
    public class CreateCustomerRequest
    {
        [JsonPropertyName("DisplayName")]
        public string DisplayName { get; set; }
    }
}
