using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Payments;

public class MetaDataDto
{
    [JsonProperty("CreateTime")]
    public string CreateTime { get; set; }
    
    [JsonProperty("LastUpdatedTime")]
    public string LastUpdatedTime { get; set; }
}