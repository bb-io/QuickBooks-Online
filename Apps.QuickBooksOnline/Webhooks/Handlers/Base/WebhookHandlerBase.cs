using Apps.QuickBooksOnline.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;

namespace Apps.QuickBooksOnline.Webhooks.Handlers.Base;

public abstract class WebhookHandlerBase : AppInvocable, IWebhookEventHandler
{
    private readonly string _realmId;

    protected WebhookHandlerBase(InvocationContext invocationContext) : base(invocationContext)
    {
        _realmId = Creds.Get(CredNames.CompanyId).Value;
    }

    public abstract string SubscriptionEvent { get; set; }

    public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
    {        
        var bridgeService = new BridgeService(InvocationContext.UriInfo.BridgeServiceUrl.ToString());
        await bridgeService.Subscribe(values["payloadUrl"], _realmId, SubscriptionEvent);
    }

    public Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
    {
        var bridgeService = new BridgeService(InvocationContext.UriInfo.BridgeServiceUrl.ToString());
        return bridgeService.Unsubscribe(values["payloadUrl"], _realmId, SubscriptionEvent);
    }
}