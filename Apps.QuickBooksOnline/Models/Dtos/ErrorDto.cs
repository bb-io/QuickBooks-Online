using System.Text;

namespace Apps.QuickBooksOnline.Models.Dtos;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;


public class ErrorDto
{
    [JsonProperty("Fault")]
    public Fault Fault { get; set; }

    [JsonProperty("time")]
    public DateTime Time { get; set; }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append($"Type: {Fault.Type}; ");
        
        foreach (var error in Fault.Error)
        {
            stringBuilder.Append($"Message : {error.Message}, ");
            stringBuilder.Append($"Detail: {error.Detail}, ");
            stringBuilder.Append($"Code: {error.Code}; ");
        }
        
        return stringBuilder.ToString();
    }
}


public class ErrorDetail
{
    [JsonProperty("Message")]
    public string Message { get; set; }

    [JsonProperty("Detail")]
    public string Detail { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }
}

public class Fault
{
    [JsonProperty("Error")]
    public List<ErrorDetail> Error { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}