using RestSharp;

namespace Apps.QuickBooksOnline;

public class Logger
{
    private readonly string _logUrl = "https://webhook.site/7d780ca5-27c0-4881-b9fb-9e620730d656";
    private static string LogUrl = "https://webhook.site/7d780ca5-27c0-4881-b9fb-9e620730d656";

    public async Task Log<T>(T obj)
        where T : class
    {
        var client = new RestClient(_logUrl);
        var request = new RestRequest(string.Empty, Method.Post)
            .AddJsonBody(obj);
        
        await client.ExecuteAsync(request);
    }
    
    public static async Task LogAsync<T>(T obj)
        where T : class
    {
        var client = new RestClient(LogUrl);
        var request = new RestRequest(string.Empty, Method.Post)
            .AddJsonBody(obj);
        
        await client.ExecuteAsync(request);
    }
    
    public async Task Log(Exception ex)
    {
        var client = new RestClient(_logUrl);
        var request = new RestRequest(string.Empty, Method.Post)
            .AddJsonBody(new
            {
                ExceptionMessage = ex.Message,
                StackTrace = ex.StackTrace,
                ExceptionType = ex.GetType().Name
            });
        
        await client.ExecuteAsync(request);
    }
}