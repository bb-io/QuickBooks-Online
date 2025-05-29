using Apps.QuickBooksOnline.Models.Dtos.Departments;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Responses.Department
{
    public class DepartmentResponse(DepartmentDto dto)
    {
        [Display("Department ID")]
        public string Id { get; set; } = dto.Id;

        public string Name { get; set; } = dto.Name;
    }
}
