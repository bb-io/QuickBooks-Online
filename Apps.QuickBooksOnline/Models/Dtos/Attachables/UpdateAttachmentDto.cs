using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Attachables;

public class UpdateAttachmentDto : CreateAttachmentDto
{
    [JsonProperty("Id")]
    public string Id { get; set; }
    
    [JsonProperty("SyncToken")]
    public string SyncToken { get; set; }
}