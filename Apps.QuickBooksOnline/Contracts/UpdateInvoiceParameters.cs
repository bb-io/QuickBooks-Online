namespace Apps.QuickBooksOnline.Contracts
{
    public class UpdateInvoiceParameters : CreateInvoiceParameters
    {
        public string InvoiceId { get; set; }
        public string SyncToken { get; set; }
    }
}
