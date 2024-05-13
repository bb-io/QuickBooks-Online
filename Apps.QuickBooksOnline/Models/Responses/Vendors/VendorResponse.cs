using Apps.QuickBooksOnline.Models.Dtos.Vendors;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Responses.Vendors;

public class VendorResponse(VendorDto dto)
{
    [Display("Vendor ID")]
    public string Id { get; set; } = dto.Id;

    public double Balance { get; set; } = dto.Balance;

    [Display("Vendor 1099")]
    public bool Vendor1099 { get; set; } = dto.Vendor1099;

    public string Currency { get; set; } = dto.CurrencyRef.Name;

    public string Domain { get; set; } = dto.Domain;

    public bool Sparse { get; set; } = dto.Sparse;

    [Display("Sync token")]
    public string SyncToken { get; set; } = dto.SyncToken;

    [Display("Created at")]
    public DateTime CreatedAt { get; set; } = DateTime.Parse(dto.MetaData.CreateTime);

    [Display("Display name")]
    public string DisplayName { get; set; } = dto.DisplayName;

    [Display("Print on check name")]
    public string PrintOnCheckName { get; set; } = dto.PrintOnCheckName;

    public bool Active { get; set; } = dto.Active;

    [Display("Primary phone number")]
    public string PrimaryPhoneNumber { get; set; } = dto.PrimaryPhone?.FreeFormNumber  ?? string.Empty;

    [Display("Mobile phone number")]
    public string Mobile { get; set; } = dto.Mobile?.FreeFormNumber  ?? string.Empty;

    [Display("Fax number")]
    public string Fax { get; set; } = dto.Fax?.FreeFormNumber  ?? string.Empty;

    [Display("Web address")]
    public string WebAddrUrl { get; set; } = dto.WebAddr?.URI  ?? string.Empty;

    public BillAddrResponse BillAddr { get; set; } = new()
    {
        Id = dto.BillAddr?.Id ?? string.Empty,
        Line1 = dto.BillAddr?.Line1 ?? string.Empty,
        City = dto.BillAddr?.City ?? string.Empty, 
        PostalCode = dto.BillAddr?.PostalCode  ?? string.Empty
    };

    [Display("Term refence")]
    public string TermRef { get; set; } = dto.TermRef?.value ?? string.Empty;

    [Display("Company name")]
    public string CompanyName { get; set; } = dto.CompanyName;

    [Display("Given name")]
    public string GivenName { get; set; } = dto.GivenName;

    [Display("Family name")]
    public string FamilyName { get; set; } = dto.FamilyName;

    public string Country { get; set; } = dto.Country;

    [Display("Postal code")]
    public string PostalCode { get; set; } = dto.BillAddr?.PostalCode ?? string.Empty;
}

public class BillAddrResponse
{
    [Display("Bill address ID")]
    public string Id { get; set; }
    
    [Display("Line 1")]
    public string Line1 { get; set; }
    
    public string City { get; set; }
    
    [Display("Postal code")]
    public string PostalCode { get; set; }
}