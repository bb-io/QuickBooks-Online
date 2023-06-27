using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Microsoft.AspNetCore.WebUtilities;

namespace Apps.QuickBooksOnline.Auth
{
    public class OAuth2AuthorizeService : IOAuth2AuthorizeService
    {
        public string GetAuthorizationUrl(Dictionary<string, string> values)
        {
            const string oauthUrl = "https://appcenter.intuit.com/connect/oauth2";
            var parameters = new Dictionary<string, string>
            {
                { "client_id", values["client_id"] },
                { "response_type", "code" },
                { "scope", values["scope"] },
                { "redirect_uri", values["redirect_uri"] },
                { "state", values["state"] }
            };
            return QueryHelpers.AddQueryString(oauthUrl, parameters);
        }
    }
}
