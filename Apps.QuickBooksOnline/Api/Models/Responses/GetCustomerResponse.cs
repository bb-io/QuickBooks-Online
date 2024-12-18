using Apps.QuickBooksOnline.Dtos;
using Apps.QuickBooksOnline.Models.Responses.Vendors;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Api.Models.Responses;

public class GetCustomerResponse
{
    public GetCustomerResponse(CustomerDto customer)
    {
        Id = customer.Id;
        DisplayName = customer.DisplayName;
        SyncToken = customer.SyncToken;
        EmailAddress = customer.PrimaryEmailAddr?.Address;
        GivenName = customer.GivenName;
        FamilyName = customer.FamilyName;
        CompanyName = customer.CompanyName;
        FullyQualifiedName = customer.FullyQualifiedName;
        Active = customer.Active;
        Taxable = customer.Taxable;
        Job = customer.Job;
        BillingAddress = new BillAddrResponse
        {
            Line1 = customer.BillAddr?.Line1,
            City = customer.BillAddr?.City,
            PostalCode = customer.BillAddr?.PostalCode,
        };
        Balance = customer.Balance; 
    }

    [Display("Customer ID")]
    public string Id { get; set; }

    [Display("Display name")]
    public string DisplayName { get; set; }

    [Display("Sync token")]
    public string SyncToken { get; set; }

    [Display("Email address")]
    public string EmailAddress { get; set; }
    
    [Display("Given name")]
    public string GivenName { get; set; }
    
    [Display("Family name")]
    public string FamilyName { get; set; }
    
    [Display("Company name")]
    public string CompanyName { get; set; }
    
    [Display("Fully qualified name")]
    public string FullyQualifiedName { get; set; }

    public string Domain { get; set; }

    [Display("Primary phone")]
    public string PrimaryPhone { get; set; }

    public bool Active { get; set; }
    
    public bool Taxable { get; set; }
    
    public bool Job { get; set; }

    [Display("Billing address")]
    public BillAddrResponse BillingAddress { get; set; }

    public double Balance { get; set; }
}