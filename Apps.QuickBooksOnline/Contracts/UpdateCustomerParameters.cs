namespace Apps.QuickBooksOnline.Contracts
{
    public class UpdateCustomerParameters
    {
        public string DisplayName { get; set; }
        public string CustomerId { get; set; }
        public string SyncToken { get; set; }
    }
}
