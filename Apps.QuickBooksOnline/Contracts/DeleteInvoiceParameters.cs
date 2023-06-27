namespace Apps.QuickBooksOnline.Contracts
{
    public class DeleteInvoiceParameters
    {
        public string InvoiceId { get; set; }
        public string SyncToken { get; set; }
    }
}
