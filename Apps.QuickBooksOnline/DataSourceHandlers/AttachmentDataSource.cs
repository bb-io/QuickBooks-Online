using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Models.Responses.Attachable;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.DataSourceHandlers;

public class AttachmentDataSource(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var classActions = new AttachmentActions(InvocationContext, null);
        
        var classesResponse = await classActions.GetAllAttachments();
        
        return classesResponse.Attachments
            .Where(x => context.SearchString == null ||
                        BuildReadableName(x).Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.Id, BuildReadableName);
    }
    
    private string BuildReadableName(AttachmentResponse attachment)
    {
        if(attachment.Note != null)
        {
            string note = attachment.Note.Length > 20 ? attachment.Note.Substring(0, 20) + "..." : attachment.Note;
            return $"[{attachment.Id}] - {note}";
        }
        
        return $"[{attachment.Id}]";
    }
}