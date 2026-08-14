using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Apps.QuickBooksOnline.Auth;

public class OAuth2TokenService(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IOAuth2TokenService, ITokenRefreshable
{
    private const string ExpiresAtKeyName = "expires_at";

    public bool IsRefreshToken(Dictionary<string, string> values)
    {
        var expiresAt = DateTime.Parse(values[ExpiresAtKeyName]);
        return DateTime.UtcNow > expiresAt;
    }

    public int? GetRefreshTokenExprireInMinutes(Dictionary<string, string> values)
    {
        if (!values.TryGetValue(ExpiresAtKeyName, out var expireValue))
            return null;

        if (!DateTime.TryParse(expireValue, out var expireDate))
            return null;

        var difference = expireDate - DateTime.UtcNow;

        return (int)difference.TotalMinutes - 5;
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

        var credentials = ApplicationConstants.GetCreds(values);
        return await RequestToken(bodyParameters, credentials.ClientId, credentials.ClientSecret, cancellationToken);
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
            { "redirect_uri", $"{InvocationContext.UriInfo.BridgeServiceUrl.ToString().TrimEnd('/')}/AuthorizationCode" },
            { "code", code }
        };
        
        var credentials = ApplicationConstants.GetCreds(values);
        return await RequestToken(bodyParameters, credentials.ClientId, credentials.ClientSecret, cancellationToken);
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

        var oAuthEndpoints = await GetOAuthEndpointsAsync();
        using var response =
            await httpClient.PostAsync(oAuthEndpoints.TokenEndpoint, new FormUrlEncodedContent(bodyParameters), cancellationToken);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw BuildTokenException(response.StatusCode, content);

        if (string.IsNullOrWhiteSpace(content))
            throw new PluginApplicationException(
                $"QuickBooks returned an empty response from the token endpoint (status code: {(int)response.StatusCode} {response.StatusCode}). Please try again, and reconnect your connection if the problem persists.");

        Dictionary<string, string> resultDictionary;
        try
        {
            resultDictionary = JsonSerializer
                                   .Deserialize<Dictionary<string, JsonElement>>(content)
                                   ?.ToDictionary(r => r.Key, r => r.Value.ToString())
                               ?? throw new PluginApplicationException(
                                   $"Invalid response content from the QuickBooks token endpoint: {content}");
        }
        catch (JsonException)
        {
            throw new PluginApplicationException(
                $"Failed to parse the QuickBooks token endpoint response (status code: {(int)response.StatusCode} {response.StatusCode}). Response content: {content}");
        }

        if (!resultDictionary.TryGetValue("expires_in", out var expiresIn) ||
            !int.TryParse(expiresIn, out var expiresInSeconds))
            throw new PluginApplicationException(
                $"The QuickBooks token endpoint response did not contain a valid 'expires_in' value. Response content: {content}");

        resultDictionary[ExpiresAtKeyName] = DateTime.UtcNow.AddSeconds(expiresInSeconds).ToString();
        return resultDictionary;
    }

    private static Exception BuildTokenException(HttpStatusCode statusCode, string content)
    {
        var details = string.IsNullOrWhiteSpace(content) ? "no response content" : content;

        if (statusCode is HttpStatusCode.BadRequest or HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
            return new PluginMisconfigurationException(
                $"You are not connected to QuickBooks anymore. Please reconfigure your connection. Status code: {(int)statusCode} {statusCode}, details: {details}");

        return new PluginApplicationException(
            $"QuickBooks token request failed with status code: {(int)statusCode} {statusCode}, details: {details}");
    }
}