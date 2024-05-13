using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Requests;

public class SyncTokenRequest
{
    [Display("Sync Token", Description = "By default it set to 0")]
    public string? SyncToken { get; set; }
}