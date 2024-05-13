using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Requests.Payments;

public class SendPaymentRequest : PaymentRequest
{
    [Display("Email address", Description = "Email address to send the payment to.")]
    public string EmailAddress { get; set; }
}