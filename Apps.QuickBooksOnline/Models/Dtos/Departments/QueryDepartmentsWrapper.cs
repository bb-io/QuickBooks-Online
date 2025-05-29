
using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Departments;

public class QueryDepartmentsWrapper
{
    [JsonProperty("QueryResponse")]
    public DepartmentsWrapper QueryResponse { get; set; }
}

public class DepartmentsWrapper
{
    [JsonProperty("Department")]
    public List<DepartmentDto> Department { get; set; }
}
