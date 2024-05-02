using Apps.QuickBooksOnline.Models.Dtos.Payments;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Responses;

public class PaymentResponse(PaymentCreatedDto dto)
{
    [Display("Id")]
    public string Id { get; set; } = dto.Payment.Id;

    [Display("Customer ID")]
    public string CustomerId { get; set; } = dto.Payment.CustomerRef.Value;

    [Display("Customer name")]
    public string CustomerName { get; set; } = dto.Payment.CustomerRef.Name;

    [Display("Total amount")]
    public double TotalAmount { get; set; } = (double)dto.Payment.TotalAmt;

    [Display("Unapplied amount")]
    public double UnappliedAmount { get; set; } = (double)dto.Payment.UnappliedAmt;

    [Display("Process payment")]
    public bool ProcessPayment { get; set; } = dto.Payment.ProcessPayment;

    [Display("Currency")]
    public string Currency { get; set; } = dto.Payment.CurrencyRef.Value;

    [Display("Currency name")]
    public string CurrencyName { get; set; } = dto.Payment.CurrencyRef.Name;
}