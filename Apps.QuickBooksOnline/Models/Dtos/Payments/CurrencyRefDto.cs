using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Payments;

public class CurrencyRefDto
{
    [JsonProperty("value")]
    public string Value { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
}