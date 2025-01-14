
using Apps.QuickBooksOnline.Models.Dtos.TaxCodes;
using Apps.QuickBooksOnline.Models.Responses.TaxCode;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.QuickBooksOnline.DataSourceHandlers;

public class TaxCodeDataHandler(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var sql = "select * from TaxCode";
        var taxCodeWrapper =
            await Client.ExecuteWithJson<QueryTaxCodesWrapper>($"/query?query={sql}", Method.Get, null, Creds);

        var taxcodeResponse = new GetAllTaxCodesResponse(taxCodeWrapper.QueryResponse.TaxCode);

        return taxcodeResponse.TaxCodes
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.Id, x => x.Name);
    }
}