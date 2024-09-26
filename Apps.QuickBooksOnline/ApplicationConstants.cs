using Apps.QuickBooksOnline.Constants;

namespace Apps.QuickBooksOnline;

public class ApplicationConstants
{
    public const string AppName = "quickbooks";

    public const string DeveloperClientId = "#{QUICKBOOKS_DEVELOPER_CLIENTID}#";
    public const string DeveloperClientSecret = "#{QUICKBOOKS_DEVELOPER_CLIENTSECRET}#"; 

    public const string ProductionClientId = "#{QUICKBOOKS_PRODUCTION_CLIENTID}#";
    public const string ProductionClientSecret = "#{QUICKBOOKS_PRODUCTION_CLIENTSECRET}#";

    public const string Scope = "#{QUICKBOOKS_SCOPE}#";
    
    public const string BlackbirdToken = "#{QUICKBOOKS_BLACKBIRD_TOKEN}#";

    public static Creds GetCreds(Dictionary<string, string> dictionary)
    {
        var url = dictionary[CredNames.ApiUrl];
        if (url.Contains("sandbox"))
        {
            return new Creds
            {
                ClientId = DeveloperClientId,
                ClientSecret = DeveloperClientSecret,
                Scope = Scope
            };
        } else
        {
            return new Creds
            {
                ClientId = ProductionClientId,
                ClientSecret = ProductionClientSecret,
                Scope = Scope
            };
        }
    }
}

public class Creds
{
    public string ClientId { get; set; }
    
    public string ClientSecret { get; set; }
    
    public string Scope { get; set; }
}