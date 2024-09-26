using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Models.Requests.Customers;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.Connections
{
    public class ConnectionValidator : IConnectionValidator
    {
        public async ValueTask<ConnectionValidationResponse> ValidateConnection(
            IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
        {
            return new() { IsValid = true, Message = "Success" };
            //try
            //{
            //    var customerActions = new CustomerActions(new InvocationContext() { AuthenticationCredentialsProviders = authProviders });
            //    await customerActions.GetAllCustomers(new GetCustomerFilterRequest());

            //    return new ConnectionValidationResponse
            //    {
            //        IsValid = true
            //    };
            //}
            //catch (Exception ex)
            //{
            //    return new ConnectionValidationResponse
            //    {
            //        IsValid = false,
            //        Message = ex.Message
            //    };
            //}
        }
    }
}
