using Apps.QuickBooksOnline.Models.Dtos.Payments;
using Apps.QuickBooksOnline.Models.Requests;
using Apps.QuickBooksOnline.Models.Requests.Payments;
using Apps.QuickBooksOnline.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.QuickBooksOnline.Actions;

[ActionList]
public class PaymentActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Get all payments", Description = "Get all payments")]
    public async Task<PaymentsResponse> GetAllPayments()
    {
        string sql =
            "select * from Payment Where Metadata.LastUpdatedTime>'2015-01-16' Order By Metadata.LastUpdatedTime";
        var response = await Client.ExecuteWithJson<PaymentsWrapper>($"/query?query={sql}", Method.Get, null, Creds);
        return new PaymentsResponse
        {
            Payments = response.QueryResponse.Payment.Select(x => new PaymentResponse(x)).ToList()
        };
    }

    [Action("Get payment", Description = "Get payment by ID")]
    public async Task<PaymentResponse> GetPayment([ActionParameter] PaymentRequest request)
    {
        var response =
            await Client.ExecuteWithJson<GetPaymentDto>($"/payment/{request.PaymentId}", Method.Get, null, Creds);
        return new PaymentResponse(response.Payment);
    }

    [Action("Create payment", Description = "Create a payment")]
    public async Task<PaymentResponse> CreatePayment([ActionParameter] CreatePaymentRequest request)
    {
        if (string.IsNullOrEmpty(request.CustomerId) && string.IsNullOrEmpty(request.JobId))
        {
            throw new Exception("One of the following fields must be provided: Customer ID, Job ID");
        }

        var body = new
        {
            TotalAmt = request.TotalAmount,
            CustomerRef = new
            {
                value = request.CustomerId ?? request.JobId
            }
        };

        var response = await Client.ExecuteWithJson<GetPaymentDto>("/payment", Method.Post, body, Creds);
        return new PaymentResponse(response.Payment);
    }

    [Action("Void payment", Description = "Void payment by ID")]
    public async Task<PaymentResponse> VoidPayment([ActionParameter] PaymentRequest request)
    {
        var body = new
        {
            Id = request.PaymentId,
            SyncToken = request.SyncToken ?? "0",
            sparse = true
        };

        var response =
            await Client.ExecuteWithJson<GetPaymentDto>($"/payment?operation=update&include=void", Method.Post, body,
                Creds);
        return new PaymentResponse(response.Payment);
    }

    [Action("Delete payment", Description = "Delete payment by ID")]
    public async Task DeletePayment([ActionParameter] PaymentRequest request)
    {
        var body = new
        {
            Id = request.PaymentId,
            SyncToken = request.SyncToken ?? "0"
        };

        await Client.ExecuteWithJson<object>($"/payment", Method.Post, body, Creds);
    }

    [Action("Send payment", Description = "Send payment to email")]
    public async Task<PaymentResponse> SendPayment([ActionParameter] SendPaymentRequest request)
    {
        var response = await Client.ExecuteWithJson<GetPaymentDto>($"/payment/{request.PaymentId}/send?sendTo={request.EmailAddress}", Method.Post, null, Creds);
        return new PaymentResponse(response.Payment);
    }
}