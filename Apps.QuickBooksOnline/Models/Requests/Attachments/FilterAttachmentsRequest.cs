using Apps.QuickBooksOnline.DataSourceHandlers;
using Apps.QuickBooksOnline.DataSourceHandlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests.Attachments;

public class FilterAttachmentsRequest
{
    [Display("Entity type"), StaticDataSource(typeof(ReferenceTypeDataSource))]
    public string? EntityType { get; set; }
    
    [Display("Entity ID"), DataSource(typeof(ReferenceDataSource))]
    public string? EntityId { get; set; }
}