using Apps.QuickBooksOnline.Models.Dtos.Departments;

namespace Apps.QuickBooksOnline.Models.Responses.Department;

public class GetAllDepartmentsResponse
{
    public List<DepartmentResponse> Departments { get; set; }
    
    public GetAllDepartmentsResponse(List<DepartmentDto> dtos)
    {
        Departments = dtos.Select(dto => new DepartmentResponse(dto)).ToList();
    }
}