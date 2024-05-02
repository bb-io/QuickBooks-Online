using RestSharp;
using Apps.QuickBooksOnline.Api;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.QuickBooksOnline.Connections
{
    public class ConnectionValidator : IConnectionValidator
    {
        private readonly QuickBooksClient _client = new QuickBooksClient();

        public async ValueTask<ConnectionValidationResponse> ValidateConnection(
            IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
        {
            try
            {
                await _client.ExecuteWithJson("/customers", Method.Get, null, authProviders.ToArray());
                
                return new ConnectionValidationResponse
                {
                    IsValid = true,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ConnectionValidationResponse
                {
                    IsValid = false,
                    Message = ex.Message
                };
            }
        }
    }
}
