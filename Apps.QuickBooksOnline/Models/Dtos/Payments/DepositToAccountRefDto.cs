using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Payments;

public class DepositToAccountRefDto
{
    [JsonProperty("value")]
    public string Value { get; set; }
}