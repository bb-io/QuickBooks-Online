using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Requests.Invoices;

public class SetCustomFieldRequest : GetCustomFieldRequest
{
    [Display("Custom field value")]
    public string CustomFieldValue { get; set; }
}