using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Webhooks.Models.Responses;

public class OnPaymentsDeletedResponse
{
    [Display("Payment IDs")]
    public IEnumerable<string> PaymentIds { get; set; }
}