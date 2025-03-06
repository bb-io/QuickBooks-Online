using Apps.QuickBooksOnline.Models.Dtos.Accounts;

namespace Apps.QuickBooksOnline.Models.Responses.Accounts;

public class GetAllAccountsResponse
{
    public List<AccountResponse> Accounts { get; set; }
    
    public GetAllAccountsResponse(List<AccountDto> dtos)
    {
        Accounts = dtos.Select(dto => new AccountResponse(dto)).ToList();
    }
}