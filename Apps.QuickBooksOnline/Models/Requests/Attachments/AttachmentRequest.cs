using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests.Attachments;

public class AttachmentRequest : SyncTokenRequest
{
    [Display("Attachment ID"), DataSource(typeof(AttachmentDataSource))]
    public string AttachmentId { get; set; }
}