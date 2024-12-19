using System.Net;
using System.Xml.Serialization;
using Apps.QuickBooksOnline.Constants;
using Apps.QuickBooksOnline.Extensions;
using Apps.QuickBooksOnline.Models.Dtos;
using Apps.QuickBooksOnline.Models.Responses.Attachable;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace Apps.QuickBooksOnline.Api;

public class QuickBooksClient : RestClient
{
    public async Task<T> ExecuteWithJson<T>(string endpoint, Method method, object? bodyObj,
        AuthenticationCredentialsProvider[] creds)
    {
        var response = await ExecuteWithJson(endpoint, method, bodyObj, creds);
        return JsonConvert.DeserializeObject<T>(response.Content);
    }

    public async Task<T> ExecuteWithMultipart<T>(string endpoint, Method method, HttpContent multipartContent,
        AuthenticationCredentialsProvider[] creds)
    {
        var url = BuildUrl(endpoint, creds);

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(url),
            Content = multipartContent
        };

        var authenticationCredentialsProvider = creds.First(p => p.KeyName == "Authorization");
        request.Headers.Add("Authorization", authenticationCredentialsProvider.Value);

        var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);

        var responseContent = await response.Content.ReadAsStringAsync();
        await Logger.LogAsync(new { ResponseCintent = $"Response Content: {responseContent}" });

        if (!response.IsSuccessStatusCode)
            throw GetError(responseContent, response.StatusCode);

        try
        {
            if (typeof(T) == typeof(IntuitResponse))
            {
                var serializer = new XmlSerializer(typeof(IntuitResponse));
                using var stringReader = new StringReader(responseContent);
                var xmlResponse = (T)serializer.Deserialize(stringReader);
                return xmlResponse;
            }
            
            return JsonConvert.DeserializeObject<T>(responseContent)!;
        }
        catch (JsonException)
        {
            throw new JsonSerializationException($"Failed to deserialize response content: {responseContent}");
        }
    }

    public async Task<RestResponse> ExecuteWithJson(string endpoint, Method method, object? bodyObj,
        AuthenticationCredentialsProvider[] creds)
    {
        var url = BuildUrl(endpoint, creds);

        var request = new QuickBooksRequest(new()
        {
            Url = url,
            Method = method
        }, creds);

        if (bodyObj is not null)
        {
            request.WithJsonBody(bodyObj, new()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        return await ExecuteRequest(request);
    }

    public async Task<RestResponse> ExecuteRequest(QuickBooksRequest request)
    {
        var response = await ExecuteAsync(request);

        if (!response.IsSuccessStatusCode)
            throw GetError(response);

        return response;
    }

    private Exception GetError(RestResponse response)
    {
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new PluginMisconfigurationException("You are not connected to Quickbooks anymore. Please reconfigure your connection.");
        }

        var tid = response.Headers?.FirstOrDefault(x => x.Name == "intuit_tid")?.Value ?? "N/A";

        try
        {
            var errorDto = JsonConvert.DeserializeObject<ErrorDto>(response.Content);
            return new Exception($"Status code: {response.StatusCode}, TID: {tid} | {errorDto}");
        }
        catch (Exception)
        {
            return new Exception($"Status code: {response.StatusCode}, TID: {tid}, Message: {response.Content}");
        }
    }

    private Exception GetError(string responseContent, HttpStatusCode statusCode)
    {
        var tid = "N/A";

        try
        {
            var errorDto = JsonConvert.DeserializeObject<ErrorDto>(responseContent);
            return new Exception($"Status code: {statusCode}, TID: {tid} | {errorDto}");
        }
        catch (Exception)
        {
            return new Exception($"Status code: {statusCode}, TID: {tid}, Message: {responseContent}");
        }
    }

    private string BuildUrl(string endpoint, AuthenticationCredentialsProvider[] creds)
    {
        var host = creds.GetValueByName(CredNames.ApiUrl);
        var companyId = creds.GetValueByName(CredNames.CompanyId);
        var minorVersion = creds.GetValueByName(CredNames.MinorVersion);

        return endpoint.Contains('?')
            ? $"{host}/v3/company/{companyId}{endpoint}&minorversion={minorVersion}"
            : $"{host}/v3/company/{companyId}{endpoint}?minorversion={minorVersion}";
    }
}