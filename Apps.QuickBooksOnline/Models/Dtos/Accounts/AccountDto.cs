using Apps.QuickBooksOnline.Models.Dtos.Items;
using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Accounts
{
    public class AccountDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("TaxCodeRef")]
        public TaxCodeRef? TaxCodeRef { get; set; }
    }
}
