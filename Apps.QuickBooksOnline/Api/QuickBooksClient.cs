﻿using Apps.QuickBooksOnline.Constants;
using Apps.QuickBooksOnline.Exntensions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace Apps.QuickBooksOnline.Api
{
    public class QuickBooksClient() : RestClient
    {
        public async Task<T> ExecuteWithJson<T>(string endpoint, Method method, object? bodyObj,
            AuthenticationCredentialsProvider[] creds)
        {
            var response = await ExecuteWithJson(endpoint, method, bodyObj, creds);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public async Task<RestResponse> ExecuteWithJson(string endpoint, Method method, object? bodyObj,
            AuthenticationCredentialsProvider[] creds)
        {
            var baseUrl = BuildUrl(endpoint, creds);

            var request = new QuickBooksRequest(new()
            {
                Url = baseUrl,
                Method = method
            }, creds);

            if (bodyObj is not null)
                request.WithJsonBody(bodyObj, new()
                {
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                    NullValueHandling = NullValueHandling.Ignore
                });

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
            return new Exception($"Status code: {response.StatusCode}, Message: {response.Content}");
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
}