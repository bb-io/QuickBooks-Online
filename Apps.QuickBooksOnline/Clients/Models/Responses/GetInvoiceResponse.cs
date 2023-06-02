using System.Text.Json.Serialization;

namespace Apps.QuickBooksOnline.Clients.Models.Responses
{
    public class GetInvoiceResponse
    {
        [JsonPropertyName("Invoice")]
        public Invoice Invoice { get; set; }
    }
}
