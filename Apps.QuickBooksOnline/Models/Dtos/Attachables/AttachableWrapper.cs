using System.Xml.Serialization;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Attachables;

public class AttachableWrapper
{
    [JsonProperty("Attachable")]
    public AttachableDto Attachable { get; set; }
}