using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Payments;

public class PaymentDto
{
    [JsonProperty("CustomerRef")]
    public CustomerRefDto CustomerRef { get; set; }
    
    [JsonProperty("DepositToAccountRef")]
    public DepositToAccountRefDto DepositToAccountRef { get; set; }
    
    [JsonProperty("TotalAmt")]
    public decimal TotalAmt { get; set; }
    
    [JsonProperty("UnappliedAmt")]
    public decimal UnappliedAmt { get; set; }
    
    [JsonProperty("ProcessPayment")]
    public bool ProcessPayment { get; set; }
    
    [JsonProperty("domain")]
    public string Domain { get; set; }
    
    [JsonProperty("sparse")]
    public bool Sparse { get; set; }
    
    [JsonProperty("Id")]
    public string Id { get; set; }
    
    [JsonProperty("SyncToken")]
    public string SyncToken { get; set; }
    
    [JsonProperty("MetaData")]
    public MetaDataDto MetaData { get; set; }
    
    [JsonProperty("TxnDate")]
    public string TxnDate { get; set; }
    
    [JsonProperty("CurrencyRef")]
    public CurrencyRefDto CurrencyRef { get; set; }
    
    [JsonProperty("Line")]
    public List<object> Line { get; set; }
}