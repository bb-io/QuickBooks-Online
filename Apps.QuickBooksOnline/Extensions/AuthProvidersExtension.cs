using Blackbird.Applications.Sdk.Common.Authentication;

namespace Apps.QuickBooksOnline.Extensions
{
    public static class AuthProvidersExtension
    {
        public static string GetValueByName(this IEnumerable<AuthenticationCredentialsProvider> source, string name) 
            => source.FirstOrDefault(x => x.KeyName == name)?.Value
               ?? string.Empty;
    }
}
