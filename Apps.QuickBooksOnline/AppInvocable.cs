using Apps.QuickBooksOnline.Api;
using Apps.QuickBooksOnline.Models.Dtos;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.QuickBooksOnline;

public class AppInvocable(InvocationContext invocationContext) : BaseInvocable(invocationContext)
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();
    
    private static string DiscoveryUrl => "https://developer.api.intuit.com/.well-known/openid_configuration";

    protected Logger Logger { get; } = new();

    protected QuickBooksClient Client { get; } = new();
    
    protected async Task<OAuthEndpoints> GetOAuthEndpointsAsync()
    {
        var restClient = new RestClient();
        var json = await restClient.ExecuteAsync(new RestRequest(DiscoveryUrl));
        var endpoints = JsonConvert.DeserializeObject<OAuthEndpoints>(json.Content!)!;
        
        return endpoints;
    }
    
    protected OAuthEndpoints GetOAuthEndpoints()
    {
        var restClient = new RestClient();
        var json = restClient.Execute(new RestRequest(DiscoveryUrl));
        var endpoints = JsonConvert.DeserializeObject<OAuthEndpoints>(json.Content!)!;
        
        return endpoints;
    }
}