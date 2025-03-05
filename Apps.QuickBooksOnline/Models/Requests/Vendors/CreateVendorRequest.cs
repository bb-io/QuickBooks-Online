using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Requests.Vendors;

public class CreateVendorRequest
{
    [Display("Display name")]
    public string DisplayName { get; set; }

    [Display("Title")]
    public string? Title { get; set; }

    [Display("Given name")]
    public string? GivenName { get; set; }

    [Display("Middle name")]
    public string? MiddleName { get; set; }

    [Display("Family name")]
    public string? FamilyName { get; set; }

    [Display("Suffix")]
    public string? Suffix { get; set; }

    [Display("Primary email address")]
    public string? PrimaryEmailAddr { get; set; }

    [Display("Website URL")]
    public string? WebAddr { get; set; }

    [Display("Primary phone")]
    public string? PrimaryPhone { get; set; }

    [Display("Mobile phone")]
    public string? Mobile { get; set; }

    [Display("Tax identifier")]
    public string? TaxIdentifier { get; set; }

    [Display("Account number")]
    public string? AcctNum { get; set; }

    [Display("Company name")]
    public string? CompanyName { get; set; }

    [Display("Print on check name")]
    public string? PrintOnCheckName { get; set; }

    [Display("Address line 1")]
    public string? AddressLine1 { get; set; }

    [Display("Address line 2")]
    public string? AddressLine2 { get; set; }

    [Display("Address line 3")]
    public string? AddressLine3 { get; set; }

    [Display("City")]
    public string? City { get; set; }

    [Display("Postal code")]
    public string? PostalCode { get; set; }

    [Display("Country")]
    public string? Country { get; set; }

    [Display("State code")]
    public string? StateCode { get; set; }

    [Display("Currency code")]
    public string? CurrencyCode { get; set; }
}
