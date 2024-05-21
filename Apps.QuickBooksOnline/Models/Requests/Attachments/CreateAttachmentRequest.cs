using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.QuickBooksOnline.Models.Requests.Attachments;

public class CreateAttachmentRequest : FilterAttachmentsRequest
{
    public string Note { get; set; }

    public FileReference? File { get; set; }

    [Display("Include on send", Description = "Default is false.")]
    public bool? IncludeOnSend { get; set; }
}