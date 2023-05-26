using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Apps.Intuit.Auth
{
    public class OAuth2TokenService : IOAuth2TokenService
    {
        private const string ExpiresAtKeyName = "expires_at";
        private const string TokenUrl = "https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer";

        public bool IsRefreshToken(Dictionary<string, string> values)
        {
            var expiresAt = DateTime.Parse(values[ExpiresAtKeyName]);
            return DateTime.UtcNow > expiresAt;
        }

        public async Task<Dictionary<string, string>> RefreshToken(Dictionary<string, string> values, CancellationToken cancellationToken)
        {
            const string grant_type = "refresh_token";

            var bodyParameters = new Dictionary<string, string>
            {
                { "grant_type", grant_type },
                { "refresh_token", values["refresh_token"] },
            };

            return await RequestToken(bodyParameters, values["client_id"], values["client_secret"],  cancellationToken);
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
                { "redirect_uri", values["redirect_uri"] },
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
            var utcNow = DateTime.UtcNow;
            using HttpClient httpClient = new();
            var headerValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", headerValue);
            using var httpContent = new FormUrlEncodedContent(bodyParameters);
            using var response = await httpClient.PostAsync(TokenUrl, httpContent, cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync();
            var resultDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent)?.ToDictionary(r => r.Key, r => r.Value?.ToString())
                ?? throw new InvalidOperationException($"Invalid response content: {responseContent}");
            var expriresIn = int.Parse(resultDictionary["expires_in"]);
            var expiresAt = utcNow.AddSeconds(expriresIn);
            resultDictionary.Add(ExpiresAtKeyName, expiresAt.ToString());
            return resultDictionary;
        }
    }
}
