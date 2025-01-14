
using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.TaxCodes;

public class QueryTaxCodesWrapper
{
    [JsonProperty("QueryResponse")]
    public TaxCodesWrapper QueryResponse { get; set; }
}

public class TaxCodesWrapper
{
    [JsonProperty("TaxCode")]
    public List<TaxCodeDto> TaxCode { get; set; }
}
