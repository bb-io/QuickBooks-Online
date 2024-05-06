using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Webhooks.Payloads;

public class EventNotification
{
    [JsonProperty("realmId")]
    public string RealmId { get; set; }
    
    [JsonProperty("dataChangeEvent")]
    public DataChangeEvent DataChangeEvent { get; set; }
}