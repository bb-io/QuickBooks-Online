using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.QuickBooksOnline.Clients
{
    public class QuickBooksClient : RestClient
    {
        public QuickBooksClient(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) :
            base(new RestClientOptions() 
            {
                ThrowOnAnyError = true,
                BaseUrl = new Uri(
                    new Uri(authenticationCredentialsProviders.First(x => x.KeyName == "api_url").Value),
                    $"company/{authenticationCredentialsProviders.First(x => x.KeyName == "company_id").Value}")
            }) { }
    }
}
