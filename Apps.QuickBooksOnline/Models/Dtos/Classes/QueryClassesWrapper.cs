using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Classes;

public class QueryClassesWrapper
{
    [JsonProperty("QueryResponse")]
    public ClassesWrapper QueryResponse { get; set; }
}

public class ClassesWrapper
{
    [JsonProperty("Class")]
    public List<ClassDto> Class { get; set; }
}