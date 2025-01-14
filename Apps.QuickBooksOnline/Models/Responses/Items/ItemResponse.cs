using Apps.QuickBooksOnline.Models.Dtos.Items;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Responses.Items;

public class ItemResponse(ItemDto dto)
{
    [Display("Item ID")]
    public string Id { get; set; } = dto.Id;

    public string Name { get; set; } = dto.Name;

    public string Description { get; set; } = dto.Description;

    public string Type { get; set; } = dto.Type;

    [Display("Income account ID")]
    public string IncomingAccountId { get; set; } = dto.IncomeAccountRef?.Value;

    [Display("Income account name")]
    public string IncomeAccountName { get; set; } = dto.IncomeAccountRef?.Name;

    [Display("Tax code value")]
    public string TaxCodeValue { get; set; } = dto.SalesTaxCodeRef?.Value ?? "";

    [Display("Tax code name")]
    public string TaxCodeName { get; set; } = dto.SalesTaxCodeRef?.Name ?? "";

    [Display("Unit price")]
    public double UnitPrice { get; set; } = dto.UnitPrice;

    [Display("Purchase cost")]
    public double PurchaseCost { get; set; } = dto.PurchaseCost;
}