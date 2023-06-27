using Apps.QuickBooksOnline.Clients.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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

        [JsonPropertyName("SyncToken")]
        public string SyncToken { get; set; }
    }
}
