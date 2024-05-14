using System.Text;
using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Invoices.Common;

public class InvoicesObject
{
    [JsonProperty("invoices")]
    public List<Invoice> Invoices { get; set; } = new();
}