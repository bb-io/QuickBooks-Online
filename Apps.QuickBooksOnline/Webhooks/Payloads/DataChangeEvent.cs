using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Webhooks.Payloads;

public class DataChangeEvent
{
    [JsonProperty("entities")]
    public List<Entity> Entities { get; set; }
}