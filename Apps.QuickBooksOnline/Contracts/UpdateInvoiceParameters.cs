using Apps.QuickBooksOnline.Models.Requests.Invoices;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Contracts
{
    public class UpdateInvoiceParameters : InvoiceRequest
    {
        [Display("Due date")]
        public DateTime DueDate { get; set; }
        
        [Display("Sync token", Description = "By default, the sync token is set to 0")]
        public string? SyncToken { get; set; }
    }
}
