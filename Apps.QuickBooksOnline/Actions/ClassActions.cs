using Apps.QuickBooksOnline.Models.Dtos.Classes;
using Apps.QuickBooksOnline.Models.Requests.Classes;
using Apps.QuickBooksOnline.Models.Responses.Classes;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.QuickBooksOnline.Actions;

[ActionList]
public class ClassActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Get all classes", Description = "Get all classes")]
    public async Task<ClassesResponse> GetAllClasses()
    {
        var sql = "select * from Class";
        var classesWrapper =
            await Client.ExecuteWithJson<QueryClassesWrapper>($"/query?query={sql}", Method.Get, null, Creds);

        return new ClassesResponse(classesWrapper);
    }

    [Action("Get class", Description = "Get class by ID")]
    public async Task<ClassResponse> GetClassById([ActionParameter] ClassRequest request)
    {
        var dto = await Client.ExecuteWithJson<GetClassDto>($"/class/{request.ClassId}", Method.Get, null, Creds);
        return new ClassResponse(dto.Class);
    }
    
    [Action("Create class", Description = "Create class")]
    public async Task<ClassResponse> CreateClass([ActionParameter] CreateClassRequest request)
    {
        var body = new Dictionary<string, object> { { "Name", request.Name } };

        if (!string.IsNullOrEmpty(request.ClassReferenceId))
        {
            if (!string.IsNullOrEmpty(request.ReferenceName))
            {
                body.Add("ParentRef", new
                {
                    value = request.ClassReferenceId,
                });
            }
            else
            {
                body.Add("ParentRef", new
                {
                    value = request.ClassReferenceId,
                    name = request.ReferenceName
                });
            }
        }

        var dto = await Client.ExecuteWithJson<GetClassDto>("/class", Method.Post, body, Creds);
        return new ClassResponse(dto.Class);
    }
    
    [Action("Update class", Description = "Update class")]
    public async Task<ClassResponse> UpdateClass([ActionParameter] UpdateClassRequest request)
    {
        var dto = await Client.ExecuteWithJson<GetClassDto>($"/class/{request.ClassId}", Method.Get, null, Creds);

        dto.Class.Name = request.Name;
        if(request.Active.HasValue)
            dto.Class.Active = request.Active.Value;
        if(request.SubClass.HasValue)
            dto.Class.SubClass = request.SubClass.Value;
        if(!string.IsNullOrEmpty(request.SyncToken))
            dto.Class.SyncToken = request.SyncToken;
        if(!string.IsNullOrEmpty(request.Domain))
            dto.Class.Domain = request.Domain;
        if(!string.IsNullOrEmpty(request.FullyQualifiedName))
            dto.Class.FullyQualifiedName = request.FullyQualifiedName;

        var response = await Client.ExecuteWithJson<GetClassDto>($"/class/{request.ClassId}", Method.Post, dto, Creds);
        return new ClassResponse(response.Class);
    }
}