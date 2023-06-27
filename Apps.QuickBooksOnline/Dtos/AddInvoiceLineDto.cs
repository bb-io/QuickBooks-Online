using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Apps.QuickBooksOnline.Dtos
{
    public class AddInvoiceLineDto
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("SyncToken")]
        public string SyncToken { get; set; }

        public bool sparse { get; set; }

        [JsonPropertyName("Line")]
        public List<Dtos.Line> Line { get; set; }
    }
}
