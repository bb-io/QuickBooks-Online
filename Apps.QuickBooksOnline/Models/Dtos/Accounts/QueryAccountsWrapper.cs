
using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Accounts;

public class QueryAccountsWrapper
{
    [JsonProperty("QueryResponse")]
    public AccountsWrapper QueryResponse { get; set; }
}

public class AccountsWrapper
{
    [JsonProperty("Account")]
    public List<AccountDto> Account { get; set; }
}
