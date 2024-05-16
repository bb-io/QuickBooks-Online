using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Requests;

public class SyncTokenRequest
{
    [Display("Sync Token")]
    public string? SyncToken { get; set; }
}