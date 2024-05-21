using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Attachables;

public class AttachableDto
{
    [JsonProperty("Id")]
    public string Id { get; set; }
 
    [JsonProperty("Note")]
    public string Note { get; set; }
    
    [JsonProperty("domain")]
    public string Domain { get; set; }
    
    [JsonProperty("sparse")]
    public bool Sparse { get; set; }
    
    [JsonProperty("SyncToken")]
    public string SyncToken { get; set; }

    [JsonProperty("FileName")]
    public string? FileName { get; set; }

    [JsonProperty("TempDownloadUri")]
    public string TempDownloadUri { get; set; }
    
    [JsonProperty("ThumbnailTempDownloadUri")]
    public string ThumbnailTempDownloadUri { get; set; }

    public long Size { get; set; }
    
    [JsonProperty("MetaData")]
    public MetaData MetaData { get; set; }
    
    [JsonProperty("AttachableRef")]
    public List<AttachableRef> AttachableRef { get; set; }
}

public class AttachableRef
{
    [JsonProperty("EntityRef")]
    public EntityRef EntityRef { get; set; }
    
    [JsonProperty("IncludeOnSend")]
    public bool IncludeOnSend { get; set; }
}

public class EntityRef
{
    [JsonProperty("value")]
    public string Value { get; set; }
    
    [JsonProperty("type")]
    public string Type { get; set; }
}