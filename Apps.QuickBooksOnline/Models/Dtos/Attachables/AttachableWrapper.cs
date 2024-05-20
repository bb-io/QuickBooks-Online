using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Dtos.Attachables;

public class AttachableWrapper
{
    [Display("Attachable")]
    public AttachableDto Attachable { get; set; }
}