using Apps.QuickBooksOnline.DataSourceHandlers;
using Apps.QuickBooksOnline.Models.Requests.Invoices;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Contracts
{
    public class UpdateInvoiceParameters : InvoiceRequest
    {
        [Display("Due date")]
        public DateTime? DueDate { get; set; }
        
        [Display("Sync token", Description = "By default, the sync token is set to 0")]
        public string? SyncToken { get; set; }
        
        [Display("Class ID"), DataSource(typeof(ClassDataHandler))]
        public string? ClassReferenceId { get; set; }

        [Display("Class reference name")]
        public string? ClassReferenceName { get; set; }
    }
}
