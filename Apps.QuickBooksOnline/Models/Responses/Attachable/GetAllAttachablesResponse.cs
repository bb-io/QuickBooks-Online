using Apps.QuickBooksOnline.Models.Dtos.Attachables;

namespace Apps.QuickBooksOnline.Models.Responses.Attachable;

public class GetAllAttachmentsResponse
{
    public List<AttachmentResponse> Attachments { get; set; }
    
    public GetAllAttachmentsResponse(QueryAttachableWrapper classesWrapper)
    {
        Attachments = classesWrapper.QueryResponse.Attachable.Select(x => new AttachmentResponse(x)).ToList();
    }
}