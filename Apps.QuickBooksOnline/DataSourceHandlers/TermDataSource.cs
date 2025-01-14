using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Models.Dtos.Terms;
using Apps.QuickBooksOnline.Models.Dtos.Vendors;
using Apps.QuickBooksOnline.Models.Responses.Term;
using Apps.QuickBooksOnline.Models.Responses.Vendors;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.QuickBooksOnline.DataSourceHandlers;

public class TermDataSource(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var sql = "select * from Term";
        var termsWrapper =
            await Client.ExecuteWithJson<QueryTermsWrapper>($"/query?query={sql}", Method.Get, null, Creds);

        var termsResponse = new GetAllTermsResponse(termsWrapper.QueryResponse.Term);

        return termsResponse.Terms
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .Select(x => new DataSourceItem(x.Id, x.Name));
    }
}