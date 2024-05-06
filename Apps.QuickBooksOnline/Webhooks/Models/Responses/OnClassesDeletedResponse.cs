using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Webhooks.Models.Responses;

public class OnClassesDeletedResponse
{
    [Display("Class IDs")]
    public IEnumerable<string> ClassIds { get; set; }
}