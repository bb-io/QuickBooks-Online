using System.Text.Json.Serialization;

namespace Apps.QuickBooksOnline.Clients.Models.Requests
{
    public class CreateInvoiceRequest
    {
        [JsonPropertyName("CustomerRef")]
        public Customer Customer { get; set; }

        [JsonPropertyName("Line")]
        public Line[] Line { get; set; }
    }

    public class Customer
    {
        [JsonPropertyName("value")]
        public string Id { get; set; }
    }

    public class Line
    {
        [JsonPropertyName("Amount")]
        public double Amount { get; set; }

        [JsonPropertyName("DetailType")]
        public string DetailType { get; set; }

        [JsonPropertyName("SalesItemLineDetail")]
        public SalesItemLineDetail SalesItemLineDetail { get; set; }
    }

    public class SalesItemLineDetail
    {
        [JsonPropertyName("ItemRef")]
        public Item Item { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public string Id { get; set; }
    }
}
