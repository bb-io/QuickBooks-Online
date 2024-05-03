using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Invoices;

public class QueryInvoicesWrapper
{
    [JsonProperty("QueryResponse")]
    public InvoicesWrapper QueryResponse { get; set; }
}

public class InvoicesWrapper
{
    [JsonProperty("Invoice")]
    public List<InvoiceDto> Invoice { get; set; }
}