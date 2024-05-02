using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Payments;

public class PaymentCreatedDto
{
    [JsonProperty("Payment")]
    public PaymentDto Payment { get; set; }
    
    [JsonProperty("time")]
    public string Time { get; set; }
}