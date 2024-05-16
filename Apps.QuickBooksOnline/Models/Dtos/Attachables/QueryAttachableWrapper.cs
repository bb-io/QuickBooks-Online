using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Attachables;

public class QueryAttachableWrapper
{
    [JsonProperty("QueryResponse")]
    public AttachablesWrapper QueryResponse { get; set; }
}

public class AttachablesWrapper
{
    [JsonProperty("Attachable")]
    public List<AttachableDto> Attachable { get; set; }
}