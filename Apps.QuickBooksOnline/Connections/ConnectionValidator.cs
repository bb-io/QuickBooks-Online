using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Api;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.Connections
{
    public class ConnectionValidator : IConnectionValidator
    {
        private readonly QuickBooksClient _client = new QuickBooksClient();

        public async ValueTask<ConnectionValidationResponse> ValidateConnection(
            IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
        {
            var logger = new Logger();
            
            try
            {
                var customerActions = new CustomerActions(new InvocationContext() { AuthenticationCredentialsProviders = authProviders });
                await customerActions.GetAllCustomers();
                
                return new ConnectionValidationResponse
                {
                    IsValid = true,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                await logger.LogAsync(new
                {
                    exception_message = ex.Message,
                    stack_trace = ex.StackTrace,
                    exception_type = ex.GetType().ToString()
                });
                
                return new ConnectionValidationResponse
                {
                    IsValid = false,
                    Message = ex.Message
                };
            }
        }
    }
}
