﻿using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos;

public class OAuthEndpoints
{
    [JsonProperty("issuer")]
    public string Issuer { get; set; }

    [JsonProperty("authorization_endpoint")]
    public string AuthorizationEndpoint { get; set; }

    [JsonProperty("token_endpoint")]
    public string TokenEndpoint { get; set; }

    [JsonProperty("userinfo_endpoint")]
    public string UserinfoEndpoint { get; set; }

    [JsonProperty("revocation_endpoint")]
    public string RevocationEndpoint { get; set; }

    [JsonProperty("jwks_uri")]
    public string JwksUri { get; set; }

    [JsonProperty("response_types_supported")]
    public List<string> ResponseTypesSupported { get; set; }

    [JsonProperty("subject_types_supported")]
    public List<string> SubjectTypesSupported { get; set; }

    [JsonProperty("id_token_signing_alg_values_supported")]
    public List<string> IdTokenSigningAlgValuesSupported { get; set; }

    [JsonProperty("scopes_supported")]
    public List<string> ScopesSupported { get; set; }

    [JsonProperty("token_endpoint_auth_methods_supported")]
    public List<string> TokenEndpointAuthMethodsSupported { get; set; }

    [JsonProperty("claims_supported")]
    public List<string> ClaimsSupported { get; set; }
}