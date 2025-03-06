using Apps.QuickBooksOnline.Models.Dtos.Accounts;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Responses.Accounts
{
    public class AccountResponse(AccountDto dto)
    {
        [Display("Account ID")]
        public string Id { get; set; } = dto.Id;

        public string Name { get; set; } = dto.Name;
    }
}
