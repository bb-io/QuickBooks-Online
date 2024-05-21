using System.Xml.Serialization;

namespace Apps.QuickBooksOnline.Models.Responses.Attachable;

[XmlRoot("IntuitResponse", Namespace = "http://schema.intuit.com/finance/v3")]
public class IntuitResponse
{
    [XmlElement("AttachableResponse")]
    public AttachableResponse AttachableResponse { get; set; }
}

public class AttachableResponse
{
    [XmlElement("Attachable")]
    public Attachable Attachable { get; set; }
}

public class Attachable
{
    [XmlElement("Id")]
    public string Id { get; set; }

    [XmlElement("FileName")]
    public string FileName { get; set; }

    [XmlElement("Note")]
    public string Note { get; set; }

    [XmlElement("FileAccessUri")]
    public string FileAccessUri { get; set; }

    [XmlElement("TempDownloadUri")]
    public string TempDownloadUri { get; set; }

    [XmlElement("Size")]
    public int Size { get; set; }

    [XmlElement("ContentType")]
    public string ContentType { get; set; }

    [XmlElement("MetaData")]
    public MetaData MetaData { get; set; }

    [XmlArray("AttachableRef")]
    [XmlArrayItem("EntityRef")]
    public List<EntityRef> AttachableRef { get; set; }
}

public class MetaData
{
    [XmlElement("CreateTime")]
    public DateTime CreateTime { get; set; }

    [XmlElement("LastUpdatedTime")]
    public DateTime LastUpdatedTime { get; set; }
}

public class EntityRef
{
    [XmlAttribute("type")]
    public string Type { get; set; }

    [XmlText]
    public string Value { get; set; }
}
