using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Payments;

public class PaymentsWrapper
{
    [JsonProperty("QueryResponse")]
    public PaymentsDto QueryResponse { get; set; }
}

public class PaymentsDto
{
    [JsonProperty("Payment")]
    public List<PaymentDto> Payment { get; set; }
}