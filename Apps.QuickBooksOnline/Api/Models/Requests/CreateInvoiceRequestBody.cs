using Apps.QuickBooksOnline.Models.Dtos;
using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Api.Models.Requests;

public class SalesItemLineDetail
{
    [JsonProperty("ItemRef")]
    public ItemRef ItemRef { get; set; }

    [JsonProperty("Qty")]
    public long Qty { get; set; } = 1;

    [JsonProperty("UnitPrice")]
    public decimal? UnitPrice { get; set; }
}

public class SalesLine
{
    [JsonProperty("DetailType")]
    public string DetailType { get; set; }

    [JsonProperty("Amount")]
    public decimal Amount { get; set; }

    [JsonProperty("SalesItemLineDetail")]
    public SalesItemLineDetail SalesItemLineDetail { get; set; }

    [JsonProperty("Description")]
    public string? Description { get; set; }
}

public class CreateInvoiceRequestBody
{
    [JsonProperty("Line")]
    public List<SalesLine> Line { get; set; }

    [JsonProperty("CustomerRef")]
    public CustomerRef CustomerRef { get; set; }
    
    [JsonProperty("TxnDate")]
    public string? TxnDate { get; set; }
    
    [JsonProperty("DocNumber")]
    public string? DocNumber { get; set; }
}
