using Apps.QuickBooksOnline.Constants;

namespace Apps.QuickBooksOnline;

// test app for sandbox app TODO: replace this creds with appropriate values
public class ApplicationConstants
{
    public const string DeveloperClientId = "ABvIJYgSoW5LfKjoFAUPr1yPJcHJdlnjjF875Hj2wje2FWRboZ";
    
    public const string ProductionClientId = "ABvIJYgSoW5LfKjoFAUPr1yPJcHJdlnjjF875Hj2wje2FWRboZ"; // TODO: replace this creds with appropriate values

    public const string DeveloperClientSecret = "vFhvVGa0UEoXpMR8y9Q2dpe66Wch38P0dXYsc2bs"; 
    
    public const string ProductionClientSecret = "vFhvVGa0UEoXpMR8y9Q2dpe66Wch38P0dXYsc2bs"; // TODO: replace this creds with appropriate values

    public const string Scope = "com.intuit.quickbooks.accounting";
    
    public const string AppName = "quickbooks";
    
    public const string BlackbirdToken = "#{DROPBOX_BLACKBIRD_TOKEN}#";

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
        }
        
        return new Creds
        {
            ClientId = ProductionClientId,
            ClientSecret = ProductionClientSecret,
            Scope = Scope
        };
    }
}

public class Creds
{
    public string ClientId { get; set; }
    
    public string ClientSecret { get; set; }
    
    public string Scope { get; set; }
}