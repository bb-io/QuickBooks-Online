using RestSharp;

namespace Apps.QuickBooksOnline.Clients
{
    public class QuickBooksClient : RestClient
    {
        public QuickBooksClient() :
            base(new RestClientOptions() 
            {
                ThrowOnAnyError = true,
                BaseUrl = new Uri("https://sandbox-quickbooks.api.intuit.com/v3/company/4620816365305536530")
            }) { }
    }
}
