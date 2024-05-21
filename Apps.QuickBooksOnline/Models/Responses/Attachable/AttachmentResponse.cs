using Apps.QuickBooksOnline.Models.Dtos.Attachables;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Responses.Attachable;

public class AttachmentResponse
{
    public AttachmentResponse(AttachableDto dto)
    {
        Id = dto.Id;
        Note = dto.Note;
        Domain = dto.Domain;
        Sparse = dto.Sparse;
        SyncToken = dto.SyncToken;
        FileName = dto.FileName;
        AttachmentReferences = dto.AttachableRef.Select(x => new AttachmentReference
        {
            EntityId = x.EntityRef.Value,
            EntityType = x.EntityRef.Type,
            IncludeOnSend = x.IncludeOnSend
        }).ToList();
    }

    public AttachmentResponse(Attachable xmlDto)
    {
        Id = xmlDto.Id;
        FileName = xmlDto.FileName;
        Note = xmlDto.Note;
        Domain = "QBO";
        Sparse = false;
        FileName = xmlDto.FileName;
        AttachmentReferences = xmlDto.AttachableRef.Select(x => new AttachmentReference
        {
            EntityId = x.Value,
            EntityType = x.Type
        }).ToList();
    }

    [Display("Attachment ID")] public string Id { get; set; }

    [Display("Note")] public string? Note { get; set; }

    [Display("Domain")] public string Domain { get; set; }

    [Display("Sparse")] public bool Sparse { get; set; }

    [Display("Sync token")] public string SyncToken { get; set; }

    [Display("File name")] public string? FileName { get; set; }

    [Display("Attachment references")] public List<AttachmentReference> AttachmentReferences { get; set; }
}

public class AttachmentReference
{
    [Display("Entity ID")] public string EntityId { get; set; }

    [Display("Entity type")] public string EntityType { get; set; }

    [Display("Include on send")] public bool IncludeOnSend { get; set; }
}