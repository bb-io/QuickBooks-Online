using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Api.Models.Requests;

public class UpdateInvoiceRequestBody : CreateInvoiceRequestBody
{
    [JsonProperty("Id")]
    public string Id { get; set; }

    [JsonProperty("sparse")]
    public bool Sparse { get; set; } = true;
    
    [JsonProperty("SyncToken")]
    public string SyncToken { get; set; }
    
    [JsonProperty("DueDate")]
    public string? DueDate { get; set; }
}