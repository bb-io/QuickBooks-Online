using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Requests;

public class PaymentRequest
{
    [Display("Payment ID")]
    public string PaymentId { get; set; }

    [Display("Sync Token", Description = "By default it set to 0")]
    public string? SyncToken { get; set; }
}