using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests.Payments;

public class PaymentRequest : SyncTokenRequest
{
    [Display("Payment ID"), DataSource(typeof(PaymentDataSourceHandler))]
    public string PaymentId { get; set; }
}