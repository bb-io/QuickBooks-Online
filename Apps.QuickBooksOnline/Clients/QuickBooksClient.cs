using Apps.QuickBooksOnline.Constants;
using Apps.QuickBooksOnline.Exntensions;
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
                    new Uri(authenticationCredentialsProviders.GetValueByName(AppConstants.ApiUrlName)),
                    authenticationCredentialsProviders.GetValueByName(AppConstants.CompanyIdName))
            }) { }
    }
}
