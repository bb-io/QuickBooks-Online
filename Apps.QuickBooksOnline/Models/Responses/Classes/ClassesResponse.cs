using Apps.QuickBooksOnline.Models.Dtos.Classes;

namespace Apps.QuickBooksOnline.Models.Responses.Classes;

public class ClassesResponse
{
    public List<ClassResponse> Classes { get; set; }

    public ClassesResponse(QueryClassesWrapper queryClassesWrapper)
    {
        Classes = queryClassesWrapper.QueryResponse.Class.Select(x => new ClassResponse(x)).ToList();
    }
}