using Apps.QuickBooksOnline.Models.Dtos.Departments;
using Apps.QuickBooksOnline.Models.Responses.Department;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.QuickBooksOnline.DataSourceHandlers;

public class DepartmentDataSource(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var sql = "select * from Department";
        var departmentsWrapper =
            await Client.ExecuteWithJson<QueryDepartmentsWrapper>($"/query?query={sql}", Method.Get, null, Creds);

        var departmentsResponse = new GetAllDepartmentsResponse(departmentsWrapper.QueryResponse.Department);

        return departmentsResponse.Departments
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .Select(x => new DataSourceItem(x.Id, x.Name));
    }
}