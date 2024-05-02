using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Invocation;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Apps.QuickBooksOnline.Auth;

public class OAuth2TokenService : BaseInvocable, IOAuth2TokenService
{
    private const string ExpiresAtKeyName = "expires_at";
    private const string TokenUrl = "https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer";

    public OAuth2TokenService(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public bool IsRefreshToken(Dictionary<string, string> values)
    {
        var expiresAt = DateTime.Parse(values[ExpiresAtKeyName]);
        return DateTime.UtcNow > expiresAt;
    }

    public async Task<Dictionary<string, string>> RefreshToken(Dictionary<string, string> values,
        CancellationToken cancellationToken)
    {
        const string grant_type = "refresh_token";

        var bodyParameters = new Dictionary<string, string>
        {
            { "grant_type", grant_type },
            { "refresh_token", values["refresh_token"] },
        };

        return await RequestToken(bodyParameters, values["client_id"], values["client_secret"], cancellationToken);
    }

    public async Task<Dictionary<string, string>> RequestToken(
        string state,
        string code,
        Dictionary<string, string> values,
        CancellationToken cancellationToken)
    {
        const string grant_type = "authorization_code";

        var bodyParameters = new Dictionary<string, string>
        {
            { "grant_type", grant_type },
            { "redirect_uri", InvocationContext.UriInfo.AuthorizationCodeRedirectUri.ToString() },
            { "code", code }
        };
        return await RequestToken(bodyParameters, values["client_id"], values["client_secret"], cancellationToken);
    }

    public Task RevokeToken(Dictionary<string, string> values)
    {
        throw new NotImplementedException();
    }

    private async Task<Dictionary<string, string>> RequestToken(
        Dictionary<string, string> bodyParameters,
        string clientId,
        string clientSecret,
        CancellationToken cancellationToken)
    {
        using HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));

        using var response =
            await httpClient.PostAsync(TokenUrl, new FormUrlEncodedContent(bodyParameters), cancellationToken);

        var resultDictionary = JsonSerializer
                                   .Deserialize<Dictionary<string, object>>(await response.Content.ReadAsStringAsync())
                                   ?.ToDictionary(r => r.Key, r => r.Value?.ToString())
                               ?? throw new InvalidOperationException(
                                   $"Invalid response content: {await response.Content.ReadAsStringAsync()}");

        resultDictionary.Add(ExpiresAtKeyName,
            DateTime.UtcNow.AddSeconds(int.Parse(resultDictionary["expires_in"])).ToString());
        return resultDictionary;
    }
}