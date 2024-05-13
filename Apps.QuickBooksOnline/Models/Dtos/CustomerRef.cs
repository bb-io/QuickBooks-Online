using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos;

public class CustomerRef
{
    [JsonProperty("name")]
    public string Name { get; set; }
        
    [JsonProperty("value")]
    public string Value { get; set; }
}