using Apps.QuickBooksOnline.Models.Dtos.Accounts;
using Apps.QuickBooksOnline.Models.Responses.Accounts;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.QuickBooksOnline.DataSourceHandlers;

public class AccountDataSource(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var sql = "select * from Account where AccountType='Expense'";
        var accountsWrapper =
            await Client.ExecuteWithJson<QueryAccountsWrapper>($"/query?query={sql}", Method.Get, null, Creds);

        var accountsResponse = new GetAllAccountsResponse(accountsWrapper.QueryResponse.Account);

        return accountsResponse.Accounts
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .Select(x => new DataSourceItem(x.Id, x.Name));
    }
}