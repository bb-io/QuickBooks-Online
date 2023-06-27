using System.Text.Json.Serialization;

namespace Apps.QuickBooksOnline.Clients.Models.Responses
{
    public class CreateInvoiceResponse
    {
        [JsonPropertyName("Invoice")]
        public Invoice Invoice { get; set; }
    }
}