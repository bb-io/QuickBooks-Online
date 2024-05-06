using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Webhooks.Payloads;

public class QuickbooksEvent
{
    [JsonProperty("eventNotifications")]
    public List<EventNotification> EventNotification { get; set; }
}
