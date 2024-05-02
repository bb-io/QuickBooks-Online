using Apps.QuickBooksOnline.Api;
using Apps.QuickBooksOnline.Clients;
using Apps.QuickBooksOnline.Models.Dtos.Payments;
using Apps.QuickBooksOnline.Models.Requests;
using Apps.QuickBooksOnline.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.QuickBooksOnline.Actions;

[ActionList]
public class PaymentActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Create payment", Description = "Create a payment")]
    public async Task<PaymentResponse> CreatePayment([ActionParameter] CreatePaymentRequest request)
    {
        if(string.IsNullOrEmpty(request.CustomerId) && string.IsNullOrEmpty(request.JobId) )
        {
            throw new Exception("One of the following fields must be provided: Customer ID, Job ID");
        }
        
        var client = new QuickBooksClient();

        var body = new
        {
            TotalAmt = decimal.Parse(request.TotalAmount),
            CustomerRef = new
            {
                value = request.CustomerId
            }
        };
        
        var response = await Client.ExecuteWithJson<PaymentCreatedDto>("/payment", Method.Post, body, Creds);
        return new PaymentResponse(response);
    }
}