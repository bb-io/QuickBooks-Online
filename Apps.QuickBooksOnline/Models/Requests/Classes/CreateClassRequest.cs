using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Requests.Classes;

public class CreateClassRequest
{
    public string Name { get; set; }

    [Display("Class reference ID")]
    public string? ClassReferenceId { get; set; }
    
    [Display("Reference name")]
    public string? ReferenceName { get; set; }
}