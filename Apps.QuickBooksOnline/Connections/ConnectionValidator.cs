
using Apps.QuickBooksOnline.Clients.Models.Responses;
using Apps.QuickBooksOnline.Clients;
using Apps.QuickBooksOnline.Constants;
using Apps.QuickBooksOnline.Dtos;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using RestSharp;
using Apps.QuickBooksOnline.Exntensions;

namespace Apps.QuickBooksOnline.Connections
{
    public class ConnectionValidator : IConnectionValidator
    {
        public async ValueTask<ConnectionValidationResponse> ValidateConnection(
       IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
        {
            var client = new QuickBooksClient(authProviders);
            var request = new QuickBooksRequest($"/query?query=select * from Customer", Method.Get, authProviders);
            request.AddQueryParameter("minorversion", authProviders.GetValueByName(AppConstants.MinorVersionName));
            try
            {
                client.Get(request);
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
