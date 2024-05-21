using Intuit.Ipp.Diagnostics;
using RestSharp;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace Apps.QuickBooksOnline;

public class Logger : Intuit.Ipp.Diagnostics.ILogger, ILogger
{
    private readonly string _logUrl = "https://webhook.site/7d780ca5-27c0-4881-b9fb-9e620730d656";
    
    public async Task Log<T>(T obj)
        where T : class
    {
        var client = new RestClient(_logUrl);
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

    public void Log(TraceLevel idsTraceLevel, string messageToWrite)
    {
        var client = new RestClient(_logUrl);
        var request = new RestRequest(string.Empty, Method.Post)
            .AddJsonBody(new
            {
                Message = messageToWrite,
                Status = "From Intuit.Ipp.Diagnostics"
            });
        
        client.Execute(request);
    }

    public void Write(LogEvent logEvent)
    {
        var client = new RestClient(_logUrl);
        var request = new RestRequest(string.Empty, Method.Post)
            .AddJsonBody(new
            {
                Level = logEvent.Level,
                Message = logEvent.RenderMessage(),
                Status = "From serilog"
            });
        
        client.Execute(request);
    }
}