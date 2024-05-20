using Apps.QuickBooksOnline.DataSourceHandlers;
using Apps.QuickBooksOnline.DataSourceHandlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests.Attachments;

public class CreateAttachmentRequest
{
    public string Note { get; set; }

    [Display("Entity type"), StaticDataSource(typeof(ReferenceTypeDataSource))]
    public string? EntityType { get; set; }
    
    [Display("Entity ID"), DataSource(typeof(ReferenceDataSource))]
    public string? EntityId { get; set; }

    [Display("Include on send", Description = "Default is false.")]
    public bool? IncludeOnSend { get; set; }
}