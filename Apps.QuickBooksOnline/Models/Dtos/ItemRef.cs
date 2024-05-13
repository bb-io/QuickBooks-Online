using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos;

public class ItemRef
{
    [JsonProperty("name")]
    public string Name { get; set; }
        
    [JsonProperty("value")]
    public string Value { get; set; }
    
    [JsonProperty("ClassRef")]
    public ClassRef? ClassRef { get; set; }
}