using System.Text.Json.Serialization;

namespace Apps.QuickBooksOnline.Clients.Models.Responses
{
    public class Invoice
    {
        [JsonPropertyName("CustomerRef")]
        public InvoiceCustomer Customer { get; set; }

        [JsonPropertyName("Line")]
        public Line[] Line { get; set; }

        [JsonPropertyName("ShipFromAddr")]
        public ShipFromAddress ShipFromAddress { get; set; }

        [JsonPropertyName("PrintStatus")]
        public string PrintStatus { get; set; }

        [JsonPropertyName("EmailStatus")]
        public string EmailStatus { get; set; }

        [JsonPropertyName("Balance")]
        public double Balance { get; set; }

        [JsonPropertyName("SyncToken")]
        public string SyncToken { get; set; }

        [JsonPropertyName("Id")]
        public string InvoiceId { get; set; }
    }

    public class InvoiceCustomer
    {
        [JsonPropertyName("value")]
        public string Id { get; set; }

        public string Name { get; set; }
    }

    public class Line
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("LineNum")]
        public int LineNumber { get; set; }

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

    public class ShipFromAddress
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("Line1")]
        public string Line1 { get; set; }

        [JsonPropertyName("Line2")]
        public string Line2 { get; set; }
    }
}
