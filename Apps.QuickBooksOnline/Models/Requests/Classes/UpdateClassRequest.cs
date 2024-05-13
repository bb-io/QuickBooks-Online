using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Requests.Classes;

public class UpdateClassRequest : ClassRequest
{
    public string Name { get; set; }
    
    public string? SyncToken { get; set; }
    
    public bool? SubClass { get; set; }
    
    public bool? Active { get; set; }

    public string? Domain { get; set; }

    [Display("Fully qualified name")]
    public string? FullyQualifiedName { get; set; }
}