namespace Apps.QuickBooksOnline.Contracts
{
    public class CreateInvoiceParameters
    {
        public string CustomerId { get; set; }

        public double LineAmount { get; set; }
        public string ItemName { get; set; }
        public string ItemId { get; set; }
    }
}
