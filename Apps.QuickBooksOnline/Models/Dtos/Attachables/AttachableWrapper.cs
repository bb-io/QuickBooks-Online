using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Attachables;

public class AttachableWrapper
{
    [JsonProperty("Attachable")]
    public AttachableDto Attachable { get; set; }
}