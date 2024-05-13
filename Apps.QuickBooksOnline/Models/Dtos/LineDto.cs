using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos;

public class LineDto
{
    [JsonProperty("Amount")]
    public double Amount { get; set; }
    
    [JsonProperty("LinkedTxn")]
    public List<LinkedTxnDto> LinkedTxn { get; set; }
    
    [JsonProperty("LineEx")]
    public LineExDto LineEx { get; set; }
}

public class LinkedTxnDto
{
    [JsonProperty("TxnId")]
    public string TxnId { get; set; }
    
    [JsonProperty("TxnType")]
    public string TxnType { get; set; }
}

public class LineExDto
{
    [JsonProperty("any")]
    public List<AnyDto> Any { get; set; }
}

public class AnyDto
{
    [JsonProperty("name")]
    public string UrlName { get; set; }
    
    [JsonProperty("value")]
    public NameValueDto Value { get; set; }
    
    [JsonProperty("declaredType")]
    public string DeclaredType { get; set; }
    
    [JsonProperty("scope")]
    public string Scope { get; set; }
    
    [JsonProperty("nil")]
    public bool Nil { get; set; }
    
    [JsonProperty("globalScope")]
    public bool GlobalScope { get; set; }
    
    [JsonProperty("typeSubstituted")]
    public bool TypeSubstituted { get; set; }
}

public class NameValueDto
{
    [JsonProperty("Name")]
    public string Name { get; set; }
    
    [JsonProperty("Value")]
    public string Value { get; set; }
}