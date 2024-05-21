using Apps.QuickBooksOnline.Api;
using Apps.QuickBooksOnline.Constants;
using Apps.QuickBooksOnline.Models.Dtos;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Intuit.Ipp.Core;
using Intuit.Ipp.Core.Configuration;
using Intuit.Ipp.DataService;
using Intuit.Ipp.Security;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using Logger = Intuit.Ipp.Core.Configuration.Logger;

namespace Apps.QuickBooksOnline;

public class AppInvocable(InvocationContext invocationContext) : BaseInvocable(invocationContext)
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();
    
    private static string DiscoveryUrl => "https://developer.api.intuit.com/.well-known/openid_configuration";
    
    protected Logger Logger { get; } = new();

    protected QuickBooksClient Client { get; } = new();

    protected async Task<OAuthEndpoints> GetOAuthEndpointsAsync()
    {
        var restClient = new RestClient();
        var json = await restClient.ExecuteAsync(new RestRequest(DiscoveryUrl));
        var endpoints = JsonConvert.DeserializeObject<OAuthEndpoints>(json.Content!)!;
        
        return endpoints;
    }
    
    static AppInvocable()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Fatal()
            .CreateLogger();
    }
    
    protected DataService GetDataService()
    {
        var accessToken = Creds.Get("Authorization").Value;
        var realmId = Creds.Get(CredNames.CompanyId).Value;
        
        var serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, new OAuth2RequestValidator(accessToken));
        serviceContext.IppConfiguration.BaseUrl.Qbo = Creds.Get(CredNames.ApiUrl).Value;
        serviceContext.IppConfiguration.MinorVersion.Qbo = Creds.Get(CredNames.MinorVersion).Value;
        serviceContext.IppConfiguration.AdvancedLogger = new AdvancedLogger()
        {
            RequestAdvancedLog = new RequestAdvancedLog()
            {
                EnableSerilogRequestResponseLoggingForConsole = false,
                EnableSerilogRequestResponseLoggingForDebug = false,
                EnableSerilogRequestResponseLoggingForFile = false,
                EnableSerilogRequestResponseLoggingForTrace = false,
                ServiceRequestLoggingLocationForFile = null,
                CustomLogger = Logger
            }
        };

        var logger = new Intuit.Ipp.Core.Configuration.Logger
        {
            CustomLogger = Logger,
            RequestLog = new RequestLog()
            {
                EnableRequestResponseLogging = false
            }
        };
        serviceContext.IppConfiguration.Logger = logger;

        return new DataService(serviceContext);
    }
    
    protected OAuthEndpoints GetOAuthEndpoints()
    {
        var restClient = new RestClient();
        var json = restClient.Execute(new RestRequest(DiscoveryUrl));
        var endpoints = JsonConvert.DeserializeObject<OAuthEndpoints>(json.Content!)!;
        
        return endpoints;
    }
}