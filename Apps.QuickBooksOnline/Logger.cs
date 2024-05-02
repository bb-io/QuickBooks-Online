using RestSharp;

namespace Apps.QuickBooksOnline;

public class Logger
{
    private static string _logUrl = "https://webhook.site/45674b9f-0059-47a9-b5cd-542aef154dff";
    
    public async Task LogAsync(Exception e)
    {
        await LogAsync(new
        {
            Message = e.Message,
            StackTrace = e.StackTrace,
            Type = e.GetType().ToString()
        });
    }
    
    public async Task LogAsync<T>(T obj)
        where T : class
    {
        var request = new RestRequest(string.Empty, Method.Post)
            .AddJsonBody(obj);
        
        var client = new RestClient(_logUrl);
        await client.ExecuteAsync(request);
    }
}