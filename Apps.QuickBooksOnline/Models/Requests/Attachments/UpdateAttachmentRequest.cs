using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests.Attachments;

public class UpdateAttachmentRequest : FilterAttachmentsRequest
{
    [Display("Attachment ID"), DataSource(typeof(AttachmentDataSource))]
    public string AttachmentId { get; set; }
    
    public string? Note { get; set; }
    
    [Display("Include on send", Description = "Default is false.")]
    public bool? IncludeOnSend { get; set; }

    [Display("Sync token")]
    public string? SyncToken { get; set; }
}