using RestSharp;

namespace Apps.Intuit
{
    public class QuickBooksClient : RestClient
    {
        public QuickBooksClient() : base(new RestClientOptions() { ThrowOnAnyError = true, BaseUrl = new Uri("https://sandbox-quickbooks.api.intuit.com") }) { }
    }
}
