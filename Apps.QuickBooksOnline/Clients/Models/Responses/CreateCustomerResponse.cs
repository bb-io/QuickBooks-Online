﻿using System.Text.Json.Serialization;

namespace Apps.QuickBooksOnline.Clients.Models.Responses
{
    public class CreateCustomerResponse
    {
        [JsonPropertyName("Customer")]
        public Customer Customer { get; set; }
    }
}
