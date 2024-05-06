using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Webhooks.Payloads;

public class Entity
{
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("operation")]
    public string Operation { get; set; }
    
    [JsonProperty("lastUpdated")]
    public string LastUpdated { get; set; }
}