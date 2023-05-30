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
                    new ConnectionProperty("client_id"),
                    new ConnectionProperty("client_secret"),
                    new ConnectionProperty("redirect_uri"),
                    new ConnectionProperty("scope"),
                    new ConnectionProperty("response_type"),
                    new ConnectionProperty("state")
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
        }
    }
}
