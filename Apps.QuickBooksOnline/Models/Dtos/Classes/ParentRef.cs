using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Classes;

public class ParentRef
{
    [JsonProperty("value")]
    public string Value { get; set; }
}