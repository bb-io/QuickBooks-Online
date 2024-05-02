using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Contracts
{
    public class UpdateInvoiceParameters
    {
        [Display("Invoice ID")]
        public string InvoiceId { get; set; }

        [Display("Due date")]
        public DateTime DueDate { get; set; }
        
        [Display("Sync token", Description = "By default, the sync token is set to 0")]
        public string? SyncToken { get; set; }
    }
}
