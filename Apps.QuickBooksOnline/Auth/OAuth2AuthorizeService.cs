using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Invocation;
using Microsoft.AspNetCore.WebUtilities;

namespace Apps.QuickBooksOnline.Auth;

public class OAuth2AuthorizeService(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IOAuth2AuthorizeService
{
    public string GetAuthorizationUrl(Dictionary<string, string> values)
    {
        var endpoints = GetOAuthEndpoints();
        
        var credentials = ApplicationConstants.GetCreds(values);
        var parameters = new Dictionary<string, string>
        {
            { "client_id", credentials.ClientId },
            { "response_type", "code" },
            { "scope", credentials.Scope },
            { "redirect_uri", InvocationContext.UriInfo.AuthorizationCodeRedirectUri.ToString() },
            { "state", values["state"] }
        };
        
        return QueryHelpers.AddQueryString(endpoints.AuthorizationEndpoint, parameters);
    }
}