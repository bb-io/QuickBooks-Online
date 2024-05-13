using Apps.QuickBooksOnline.Models.Dtos;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Invocation;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.QuickBooksOnline.Auth;

public class OAuth2AuthorizeService(InvocationContext invocationContext)
    : BaseInvocable(invocationContext), IOAuth2AuthorizeService
{
    private static string DiscoveryUrl => "https://developer.api.intuit.com/.well-known/openid_configuration";
    
    public string GetAuthorizationUrl(Dictionary<string, string> values)
    {
        var restClient = new RestClient();
        var json = restClient.Execute(new RestRequest(DiscoveryUrl));
        var endpoints = JsonConvert.DeserializeObject<OAuthEndpoints>(json.Content!)!;
        
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