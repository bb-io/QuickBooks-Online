using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Requests.Attachments;

public class CreateAttachmentRequest : FilterAttachmentsRequest
{
    public string Note { get; set; }

    [Display("Include on send", Description = "Default is false.")]
    public bool? IncludeOnSend { get; set; }
}