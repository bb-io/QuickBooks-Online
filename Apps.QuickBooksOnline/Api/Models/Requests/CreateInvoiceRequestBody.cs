using Apps.QuickBooksOnline.Models.Dtos;
using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Api.Models.Requests;

public class SalesItemLineDetail
{
    [JsonProperty("ItemRef")]
    public ItemRef ItemRef { get; set; }
}

public class SalesLine
{
    [JsonProperty("DetailType")]
    public string DetailType { get; set; }

    [JsonProperty("Amount")]
    public decimal Amount { get; set; }

    [JsonProperty("SalesItemLineDetail")]
    public SalesItemLineDetail SalesItemLineDetail { get; set; }
}

public class CreateInvoiceRequestBody
{
    [JsonProperty("Line")]
    public List<SalesLine> Line { get; set; }

    [JsonProperty("CustomerRef")]
    public CustomerRef CustomerRef { get; set; }
}
