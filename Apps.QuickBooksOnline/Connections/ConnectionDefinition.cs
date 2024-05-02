using Apps.QuickBooksOnline.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.QuickBooksOnline.Connections;

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
                new (CredNames.ApiUrl){ DisplayName = "API url" },
                new (CredNames.CompanyId) { DisplayName = "Company ID" },
                new (CredNames.MinorVersion) { DisplayName = "Minor version" },
                new ("client_id") { DisplayName = "Client ID" },
                new ("client_secret") { DisplayName = "Client secret", Sensitive = true },
                new ("scope") { DisplayName = "Scope" }
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
            CredNames.ApiUrl,
            values[CredNames.ApiUrl]);

        yield return new AuthenticationCredentialsProvider(
            AuthenticationCredentialsRequestLocation.None,
            CredNames.CompanyId,
            values[CredNames.CompanyId]);

        yield return new AuthenticationCredentialsProvider(
            AuthenticationCredentialsRequestLocation.None,
            CredNames.MinorVersion,
            values[CredNames.MinorVersion]);
    }
}