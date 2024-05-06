using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Classes;

public class GetClassDto
{
    [JsonProperty("Class")]
    public ClassDto Class { get; set; }

    [JsonProperty("time")]
    public string Time { get; set; }
}