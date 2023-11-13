using Apps.QuickBooksOnline.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.QuickBooksOnline.Connections
{
    public class ConnectionDefinition : IConnectionDefinition
    {

        public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>()
        {
            new ConnectionPropertyGroup
            {
                Name = "OAuth2",
                AuthenticationType = ConnectionAuthenticationType.OAuth2,
                ConnectionUsage = ConnectionUsage.Actions,
                ConnectionProperties = new List<ConnectionProperty>()
                {
                    new ConnectionProperty(AppConstants.ApiUrlName){ DisplayName = "API url" },
                    new ConnectionProperty(AppConstants.CompanyIdName) { DisplayName = "Company ID" },
                    new ConnectionProperty(AppConstants.MinorVersionName) { DisplayName = "Minor version" },
                    new ConnectionProperty("client_id") { DisplayName = "Client ID" },
                    new ConnectionProperty("client_secret") { DisplayName = "Client secret", Sensitive = true },
                    new ConnectionProperty("scope") { DisplayName = "Scope" }
                }
            }
        };

        public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(Dictionary<string, string> values)
        {
            var token = values.First(v => v.Key == "access_token");
            yield return new AuthenticationCredentialsProvider(
                AuthenticationCredentialsRequestLocation.Header,
                "Authorization",
                $"bearer {token.Value}"
            );

            yield return new AuthenticationCredentialsProvider(
                AuthenticationCredentialsRequestLocation.None,
                AppConstants.ApiUrlName,
                values[AppConstants.ApiUrlName]);

            yield return new AuthenticationCredentialsProvider(
                AuthenticationCredentialsRequestLocation.None,
                AppConstants.CompanyIdName,
                values[AppConstants.CompanyIdName]);

            yield return new AuthenticationCredentialsProvider(
                AuthenticationCredentialsRequestLocation.None,
                AppConstants.MinorVersionName,
                values[AppConstants.MinorVersionName]);
        }
    }
}
