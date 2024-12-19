using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests.Customers;

public class CreateCustomerRequest
{
    [Display("Display name")]
    public string? DisplayName { get; set; }

    [Display("Company name")]
    public string? CompanyName { get; set; }

    [Display("Given name")]
    public string? GivenName { get; set; }
        
    [Display("Middle name")]
    public string? MiddleName { get; set; }
        
    [Display("Family name")]
    public string? FamilyName { get; set; }
        
    [Display("Primary email address")]
    public string? PrimaryEmailAddress { get; set; }
        
    [Display("Primary phone")]
    public string? PrimaryPhone { get; set; }

    [Display("Fully qualified name")]
    public string? FullyQualifiedName { get; set; }

    [Display("Notes")]
    public string? Notes { get; set; }

    public string? Suffix { get; set; }

    [Display("Country subdivision code")]
    public string? CountrySubDivisionCode { get; set; }

    public string? City { get; set; }
        
    [Display("Postal code")]
    public string? PostalCode { get; set; }
        
    [Display("Line 1")]
    public string? Line1 { get; set; }

    [Display("Line 2")]
    public string? Line2 { get; set; }

    public string? Country { get; set; }

    [Display("Term ID")]
    [DataSource(typeof(TermDataSource))]
    public string? Term { get; set; }

    public string? Currency { get; set; }
}