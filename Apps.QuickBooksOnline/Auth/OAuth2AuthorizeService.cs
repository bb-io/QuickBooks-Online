using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Microsoft.AspNetCore.WebUtilities;

namespace Apps.Intuit.Auth
{
    public class OAuth2AuthorizeService : IOAuth2AuthorizeService
    {
        public string GetAuthorizationUrl(Dictionary<string, string> values)
        {
            const string oauthUrl = "https://appcenter.intuit.com/connect/oauth2";
            var parameters = new Dictionary<string, string>
            {
                { "client_id", values["client_id"] },
                { "redirect_uri", values["redirect_uri"] },
                { "response_type", values["response_type"] },
                { "scope", values["scope"] },
                { "state", values["state"] }
            };
            return QueryHelpers.AddQueryString(oauthUrl, parameters);
        }
    }
}
