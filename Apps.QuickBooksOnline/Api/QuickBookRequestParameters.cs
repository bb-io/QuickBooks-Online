using RestSharp;

namespace Apps.QuickBooksOnline.Api;

public class QuickBookRequestParameters
{
    public string Url { get; set; }
    
    public Method Method { get; init; }
}