using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.QuickBooksOnline.Api;

public class QuickBooksRequest : RestRequest
{
    public QuickBooksRequest(QuickBookRequestParameters parameters, IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) : base(parameters.Url, parameters.Method)
    {
        var authenticationCredentialsProvider = authenticationCredentialsProviders.First(p => p.KeyName == "Authorization");
        this.AddHeader("Authorization", authenticationCredentialsProvider.Value);
    }
}