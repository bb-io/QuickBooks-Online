using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Invocation;
using Microsoft.AspNetCore.WebUtilities;

namespace Apps.QuickBooksOnline.Auth;

public class OAuth2AuthorizeService : BaseInvocable, IOAuth2AuthorizeService
{
    public OAuth2AuthorizeService(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public string GetAuthorizationUrl(Dictionary<string, string> values)
    {
        const string oauthUrl = "https://appcenter.intuit.com/connect/oauth2";
        var parameters = new Dictionary<string, string>
        {
            { "client_id", values["client_id"] },
            { "response_type", "code" },
            { "scope", values["scope"] },
            { "redirect_uri", InvocationContext.UriInfo.AuthorizationCodeRedirectUri.ToString() },
            { "state", values["state"] }
        };
        return QueryHelpers.AddQueryString(oauthUrl, parameters);
    }
}