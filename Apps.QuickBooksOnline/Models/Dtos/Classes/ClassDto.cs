using Apps.QuickBooksOnline.Models.Dtos.Payments;
using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Classes;

public class ClassDto
{
    [JsonProperty("Id")]
    public string Id { get; set; }
 
    [JsonProperty("Name")]
    public string Name { get; set; }
    
    [JsonProperty("SubClass")]
    public bool SubClass { get; set; }
    
    [JsonProperty("FullyQualifiedName")]
    public string FullyQualifiedName { get; set; }
    
    [JsonProperty("Active")]
    public bool Active { get; set; }
    
    [JsonProperty("domain")]
    public string Domain { get; set; }
    
    [JsonProperty("sparse")]
    public bool Sparse { get; set; }
    
    [JsonProperty("SyncToken")]
    public string SyncToken { get; set; }

    [JsonProperty("ParentRef")]
    public ParentRef ParentRef { get; set; }
    
    [JsonProperty("MetaData")]
    public MetaDataDto MetaData { get; set; }
}