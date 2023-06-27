using System.Text.Json.Serialization;

namespace Apps.QuickBooksOnline.Clients.Models.Requests
{
    public class UpdateInvoiceRequest : CreateInvoiceRequest
    {
        [JsonPropertyName("Id")]
        public string InvoiceId { get; set; }

        [JsonPropertyName("SyncToken")]
        public string SyncToken { get; set; }
    }
}
