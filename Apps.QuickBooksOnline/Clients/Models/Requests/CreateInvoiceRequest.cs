using System.Text.Json.Serialization;

namespace Apps.QuickBooksOnline.Clients.Models.Requests
{
    public class CreateInvoiceRequest
    {
        [JsonPropertyName("CustomerRef")]
        public Customer Customer { get; set; }

        public Line[] Line { get; set; }
    }

    public class Customer
    {
        [JsonPropertyName("value")]
        public string Id { get; set; }
    }

    public class Line
    {
        public double Amount { get; set; }
        public string DetailType { get; set; }
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
        public string Value { get; set; }
    }
}
