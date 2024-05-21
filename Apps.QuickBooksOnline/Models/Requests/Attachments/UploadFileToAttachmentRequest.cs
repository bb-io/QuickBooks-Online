using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.QuickBooksOnline.Models.Requests.Attachments;

public class UploadFileToAttachmentRequest : AttachmentRequest
{
    public FileReference File { get; set; }
}