namespace Apps.QuickBooksOnline.Clients.Models.Requests
{
    public class CreateCustomerRequest
    {
        public string DisplayName { get; set; }
        public string Suffix { get; set; }
        public string Title { get; set; }
        public string MiddleName { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
    }
}
