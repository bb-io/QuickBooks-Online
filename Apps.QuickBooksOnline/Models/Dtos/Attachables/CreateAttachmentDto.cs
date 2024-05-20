using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Attachables;

public class CreateAttachmentDto
{
    [JsonProperty("Note")]
    public string Note { get; set; }
    
    [JsonProperty("AttachableRef")]
    public AttachableRefDto[] AttachableRef { get; set; }
}

public class AttachableRefDto
{
    [JsonProperty("IncludeOnSend")]
    public string IncludeOnSend { get; set; }
    
    [JsonProperty("EntityRef")]
    public EntityRefDto EntityRef { get; set; }
}

public class EntityRefDto
{
    [JsonProperty("type")]
    public string Type { get; set; }
    
    [JsonProperty("value")]
    public string Value { get; set; }
}