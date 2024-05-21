using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.QuickBooksOnline.Models.Requests.Attachments;

public class UploadFileToAttachmentRequest : AttachmentRequest
{
    public FileReference File { get; set; }

    [Display("File name", Description = "You can link the already uploaded file by providing the file name.")]
    public string? FileName { get; set; }
}