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
            ConnectionProperties = new List<ConnectionProperty>()
            {
                new (CredNames.ApiUrl){ DisplayName = "API url" },
                new (CredNames.CompanyId) { DisplayName = "Company ID" },
                new (CredNames.MinorVersion) { DisplayName = "Minor version" },
            }
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(Dictionary<string, string> values)
    {
        var token = values.First(v => v.Key == "access_token");
        yield return new AuthenticationCredentialsProvider(
            "Authorization",
            $"bearer {token.Value}"
        );

        yield return new AuthenticationCredentialsProvider(
            CredNames.ApiUrl,
            values[CredNames.ApiUrl]);

        yield return new AuthenticationCredentialsProvider(
            CredNames.CompanyId,
            values[CredNames.CompanyId]);

        yield return new AuthenticationCredentialsProvider(
            CredNames.MinorVersion,
            values[CredNames.MinorVersion]);
    }
}