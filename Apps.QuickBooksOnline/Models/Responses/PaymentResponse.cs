using Apps.QuickBooksOnline.Models.Dtos.Payments;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Responses;

public class PaymentResponse(PaymentDto dto)
{
    [Display("Payment ID")]
    public string Id { get; set; } = dto.Id;

    [Display("Customer ID")]
    public string CustomerId { get; set; } = dto.CustomerRef.Value;

    [Display("Customer name")]
    public string CustomerName { get; set; } = dto.CustomerRef.Name;

    [Display("Total amount")]
    public double TotalAmount { get; set; } = (double)dto.TotalAmt;

    [Display("Unapplied amount")]
    public double UnappliedAmount { get; set; } = (double)dto.UnappliedAmt;

    [Display("Process payment")]
    public bool ProcessPayment { get; set; } = dto.ProcessPayment;

    [Display("Currency")]
    public string Currency { get; set; } = dto.CurrencyRef.Value;

    [Display("Currency name")]
    public string CurrencyName { get; set; } = dto.CurrencyRef.Name;

    public List<LineResponse> Lines { get; set; } = dto.Line.Select(x => new LineResponse(x)).ToList();
}