using Apps.QuickBooksOnline.Webhooks.Handlers.Base;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.Webhooks.Handlers;

public class PaymentWebhookHandler(InvocationContext invocationContext) : WebhookHandlerBase(invocationContext)
{
    public override string SubscriptionEvent { get; set; } = "Payment";
}