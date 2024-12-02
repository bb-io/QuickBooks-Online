
using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Terms;

public class QueryTermsWrapper
{
    [JsonProperty("QueryResponse")]
    public TermsWrapper QueryResponse { get; set; }
}

public class TermsWrapper
{
    [JsonProperty("Term")]
    public List<TermDto> Term { get; set; }
}
