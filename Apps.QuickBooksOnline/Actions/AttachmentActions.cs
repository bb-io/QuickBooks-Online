using Apps.QuickBooksOnline.Models.Dtos.Attachables;
using Apps.QuickBooksOnline.Models.Requests.Attachments;
using Apps.QuickBooksOnline.Models.Responses.Attachable;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.QuickBooksOnline.Actions;

[ActionList]
public class AttachmentActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : AppInvocable(invocationContext)
{
    [Action("Get all attachments", Description = "Get all attachments.")]
    public async Task<GetAllAttachmentsResponse> GetAllAttachments()
    {
        var sql = "select * from attachable";
        var classesWrapper =
            await Client.ExecuteWithJson<QueryAttachableWrapper>($"/query?query={sql}", Method.Get, null, Creds);

        return new GetAllAttachmentsResponse(classesWrapper);
    }
    
    [Action("Get attachment", Description = "Get attachment by ID.")]
    public async Task<AttachmentResponse> GetAttachableById([ActionParameter] AttachmentRequest request)
    {
        var attachable =
            await Client.ExecuteWithJson<AttachableDto>($"/attachable/{request.AttachmentId}", Method.Get, null, Creds);
        return new AttachmentResponse(attachable);
    }
    
    [Action("Create attachment", Description = "Create attachment.")]
    public async Task<AttachmentResponse> CreateAttachable([ActionParameter] CreateAttachmentRequest request)
    {
        var body = new
        {
            Note = request.Note,
            AttachableRef = new[]
            {
                new
                {
                    IncludeOnSend = request.IncludeOnSend ?? "false",
                    EntityRef = new
                    {
                        type = request.EntityType,
                        value = request.EntityId
                    }
                }
            }
        };
        
        var response = await Client.ExecuteWithJson<AttachableDto>("/attachable", Method.Post, body, Creds);
        return new AttachmentResponse(response);
    }
    
    [Action("Delete attachment", Description = "Delete attachment by ID.")]
    public async Task DeleteAttachableById([ActionParameter] AttachmentRequest request)
    {
        var syncToken = await GetSyncToken(request);
        var body = new
        {
            Id = request.AttachmentId,
            SyncToken = syncToken
        };
        
        await Client.ExecuteWithJson($"/attachable?operation=delete", Method.Post, body, Creds);
    }
    
    [Action("Download attachment", Description = "Download attachment by ID.")]
    public async Task<DownloadAttachmentResponse> DownloadAttachableById([ActionParameter] AttachmentRequest request)
    {
        var response = await Client.ExecuteWithJson($"/download/{request.AttachmentId}", Method.Get, null, Creds);
        var downloadUrl = response.Content;
        
        var restClient = new RestClient(downloadUrl!);
        var restRequest = new RestRequest(string.Empty, Method.Get);
        var restResponse = await restClient.ExecuteAsync(restRequest);
        
        var memoryStream = new MemoryStream(restResponse.RawBytes!);
        memoryStream.Position = 0;
        
        var attachment = await GetAttachableById(request);
        string fileName = attachment.FileName ?? restResponse.Headers?.FirstOrDefault(x => x.Name == "Content-Disposition")?.Value?.ToString()?.Split("filename=")[1] ?? "attachment";
        
        var fileReference = await fileManagementClient.UploadAsync(memoryStream, restResponse.ContentType!, fileName);
        
        return new DownloadAttachmentResponse
        {
            File = fileReference
        };
    }
    
    private async Task<string> GetSyncToken(AttachmentRequest request)
    {
        if (!string.IsNullOrEmpty(request.SyncToken))
        {
            return request.SyncToken;
        }
        
        var attachable =
            await Client.ExecuteWithJson<AttachableDto>($"/attachable/{request.AttachmentId}", Method.Get, null, Creds);
        return attachable.SyncToken;
    }
}