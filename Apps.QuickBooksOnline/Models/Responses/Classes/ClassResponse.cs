using System.Globalization;
using Apps.QuickBooksOnline.Models.Dtos.Classes;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Responses.Classes;

public class ClassResponse(ClassDto dto)
{
    [Display("Class ID")]
    public string Id { get; set; } = dto.Id;

    [Display("Name")]
    public string Name { get; set; } = dto.Name;

    [Display("Sub class")]
    public bool SubClass { get; set; } = dto.SubClass;

    [Display("Fully qualified name")]
    public string FullyQualifiedName { get; set; } = dto.FullyQualifiedName;

    [Display("Active")]
    public bool Active { get; set; } = dto.Active;

    [Display("Domain")]
    public string Domain { get; set; } = dto.Domain;

    [Display("Sparse")]
    public bool Sparse { get; set; } = dto.Sparse;

    [Display("Sync token")]
    public string SyncToken { get; set; } = dto.SyncToken;
    
    [Display("Class reference ID")]
    public string? ClassReferenceId { get; set; } = dto.ParentRef?.Value;

    [Display("Created at")]
    public DateTime CreatedAt { get; set; } = DateTime.Parse(dto.MetaData?.CreateTime ?? DateTime.Now.ToString(CultureInfo.InvariantCulture));

    [Display("Updated at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Parse(dto.MetaData?.LastUpdatedTime ?? DateTime.Now.ToString(CultureInfo.InvariantCulture));
}