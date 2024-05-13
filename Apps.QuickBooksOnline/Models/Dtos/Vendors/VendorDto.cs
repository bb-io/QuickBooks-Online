using Apps.QuickBooksOnline.Models.Dtos.Payments;
using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Vendors;

public class VendorDto
{
    public double Balance { get; set; }

    public double BillRate { get; set; }
    
    public bool Vendor1099 { get; set; }
    
    public CurrencyRefDto CurrencyRef { get; set; }
    
    [JsonProperty("domain")]
    public string Domain { get; set; }
    
    [JsonProperty("sparse")]
    public bool Sparse { get; set; }
    
    public string Id { get; set; }
    
    public string SyncToken { get; set; }
    
    public MetaDataDto MetaData { get; set; }
    
    public string DisplayName { get; set; }
    
    public string PrintOnCheckName { get; set; }
    
    public bool Active { get; set; }
    
    public PhoneDto PrimaryPhone { get; set; }

    public PrimaryEmailAddress PrimaryEmailAddr { get; set; }
    
    public PhoneDto Mobile { get; set; }
    
    public PhoneDto Fax { get; set; }
    
    public WebAddrDto WebAddr { get; set; }
    
    public BillAddrDto BillAddr { get; set; }
    
    public TermRefDto TermRef { get; set; }
    
    public string CompanyName { get; set; }
    
    public string GivenName { get; set; }
    
    public string FamilyName { get; set; }
    
    public string V4IDPseudonym { get; set; }
    
    public string TaxIdentifier { get; set; }
    
    public string AcctNum { get; set; }
    
    public string Title { get; set; }
    
    public string Suffix { get; set; }
    
    public string MiddleName { get; set; }
    
    public string Country { get; set; }
    
    public string CountrySubDivisionCode { get; set; }
    
    public string PostalCode { get; set; }
}

public class PhoneDto
{
    public string FreeFormNumber { get; set; }
}

public class WebAddrDto
{
    public string URI { get; set; }
}

public class BillAddrDto
{
    public string Id { get; set; }
    
    public string Line1 { get; set; }
    
    public string Line2 { get; set; }
    
    public string Line3 { get; set; }
    
    public string City { get; set; }
    
    public string CountrySubDivisionCode { get; set; }
    
    public string PostalCode { get; set; }
    
    public string Lat { get; set; }
    
    public string Long { get; set; }
}

public class TermRefDto
{
    public string value { get; set; }
}

public class PrimaryEmailAddress
{
    public string Address { get; set; }
}
