using System.Text.Json.Serialization;
using Apps.QuickBooksOnline.Models.Dtos.Vendors;

namespace Apps.QuickBooksOnline.Dtos
{
    public class CustomerWrapper
    {
        [JsonPropertyName("Customer")]
        public CustomerDto Customer { get; set; }
    }
    
    public class CustomerDto
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("DisplayName")]
        public string DisplayName { get; set; }

        public EmailAddressDto PrimaryEmailAddr { get; set; }
        
        public string SyncToken { get; set; }
        
        public string GivenName { get; set; }
        
        public string FamilyName { get; set; }
      
        public string CompanyName { get; set; }
        
        public string FullyQualifiedName { get; set; }
        
        public bool BillWithParent { get; set; }
        
        public bool Job { get; set; }
        
        public bool Active { get; set; }
        
        public bool Taxable { get; set; }
        
        public bool Sparse { get; set; }
        
        public double Balance { get; set; }
        
        public double BalanceWithJobs { get; set; }
        
        public BillAddrDto BillAddr { get; set; }
        
        public PhoneDto PrimaryPhone { get; set; }
        
        public string PreferredDeliveryMethod { get; set; }
        
        public string PrintOnCheckName { get; set; }
    }
    
    public class EmailAddressDto
    {
        public string Address { get; set; }
    }
}
