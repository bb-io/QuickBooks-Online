using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.QuickBooksOnline.Clients
{
    public class QuickBooksRequest : RestRequest
    {
        public QuickBooksRequest(string endpoint, Method method, IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) : base(endpoint, method)
        {
            var authenticationCredentialsProvider = authenticationCredentialsProviders.First(p => p.KeyName == "Authorization");
            this.AddHeader("Authorization", authenticationCredentialsProvider.Value);
        }
    }
}
