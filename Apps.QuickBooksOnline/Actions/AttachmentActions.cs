﻿using System.Net.Http.Headers;
using System.Text;
using Apps.QuickBooksOnline.Api;
using Apps.QuickBooksOnline.Models.Dtos.Attachables;
using Apps.QuickBooksOnline.Models.Requests.Attachments;
using Apps.QuickBooksOnline.Models.Responses.Attachable;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Newtonsoft.Json;
using RestSharp;
using Task = System.Threading.Tasks.Task;

namespace Apps.QuickBooksOnline.Actions;


[ActionList]
public class AttachmentActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    private static List<string> ApprovedFileTypes =
    [
        "ai",
        "csv",
        "doc",
        "docx",
        "eps",
        "gif",
        "jpeg",
        "jpg",
        "ods",
        "pdf",
        "png",
        "rtf",
        "tif",
        "txt",
        "xls",
        "xlsx",
        "xml"
    ];
    
    [Action("Get all attachments", Description = "Get all attachments.")]
    public async Task<GetAllAttachmentsResponse> GetAllAttachments([ActionParameter] FilterAttachmentsRequest request)
    {
        var sql = "select * from attachable";

        if (!string.IsNullOrEmpty(request.EntityType) && !string.IsNullOrEmpty(request.EntityId))
        {
            sql +=
                $" where AttachableRef.EntityRef.Type = '{request.EntityType}' and AttachableRef.EntityRef.Value = '{request.EntityId}'";
        }

        var classesWrapper =
            await Client.ExecuteWithJson<QueryAttachableWrapper>($"/query?query={sql}", Method.Get, null, Creds);

        return new GetAllAttachmentsResponse(classesWrapper);
    }

    [Action("Get attachment", Description = "Get attachment by ID.")]
    public async Task<AttachmentResponse> GetAttachableById([ActionParameter] AttachmentRequest request)
    {
        var attachable =
            await Client.ExecuteWithJson<AttachableWrapper>($"/attachable/{request.AttachmentId}", Method.Get, null,
                Creds);
        return new AttachmentResponse(attachable.Attachable);
    }

    [Action("Create attachment", Description = "Create attachment with file or note")]
    public async Task<AttachmentResponse> CreateAttachable([ActionParameter] CreateAttachmentRequest request)
    {
        var body = new CreateAttachmentDto
        {
            Note = request.Note,
            FileName = request.File?.Name,
            ContentType = request.File?.ContentType
        };

        if (request.EntityType is not null && request.EntityId is not null)
        {
            body.AttachableRef = new[]
            {
                new AttachableRefDto
                {
                    EntityRef = new EntityRefDto()
                    {
                        Type = request.EntityType,
                        Value = request.EntityId
                    }
                }
            };
        }


        if (request.File is not null)
        {
            if (!ApprovedFileTypes.Any(x => request.File.Name.EndsWith(x)))
            {
                throw new Exception("File type not supported. Supported file types are: " +
                                    string.Join(", ", ApprovedFileTypes) + ".");
            }
            
            var fileStream = await fileManagementClient.DownloadAsync(request.File);
            var fileBytes = await fileStream.GetByteData();

            var boundary = "----WebKitFormBoundary7MA4YWxkTrZu0gW";
            var multipartContent = new MultipartFormDataContent(boundary);

            var metadataContent =
                new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            metadataContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file_metadata_01",
                FileName = "attachment.json"
            };

            var fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file_content_01",
                FileName = request.File.Name
            };
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(request.File.ContentType);

            multipartContent.Add(metadataContent);
            multipartContent.Add(fileContent);

            var response =
                await Client.ExecuteWithMultipart<IntuitResponse>("/upload", Method.Post, multipartContent, Creds);
            return new AttachmentResponse(response.AttachableResponse.Attachable);
        }
        else
        {
            var response = await Client.ExecuteWithJson<AttachableWrapper>("/attachable", Method.Post, body, Creds);
            return new AttachmentResponse(response.Attachable);
        }
    }
    
    [Action("Update attachment", Description = "Update attachment by ID.")]
    public async Task<AttachmentResponse> UpdateAttachableById([ActionParameter] UpdateAttachmentRequest request)
    {
        var syncToken = await GetSyncToken(new AttachmentRequest { AttachmentId = request.AttachmentId, SyncToken = request.SyncToken });
        
        var body = new UpdateAttachmentDto
        {
            Id = request.AttachmentId,
            SyncToken = syncToken
        };
        
        if (request.Note is not null)
        {
            body.Note = request.Note;
        }
        
        if (request.IncludeOnSend is not null && request.EntityType is not null && request.EntityId is not null)
        {
            body.AttachableRef = new[]
            {
                new AttachableRefDto
                {
                    IncludeOnSend = request.IncludeOnSend.ToString(),
                    EntityRef = new EntityRefDto()
                    {
                        Type = request.EntityType,
                        Value = request.EntityId
                    }
                }
            };
        }

        var response = await Client.ExecuteWithJson<AttachableWrapper>("/attachable", Method.Post, body, Creds);
        return new AttachmentResponse(response.Attachable);
    }

    [Action("Delete attachment", Description = "Delete attachment by ID.")]
    public async Task DeleteAttachableById([ActionParameter] AttachmentRequest request)
    {
        var syncToken = await GetSyncToken(request);
        var body = new DeleteAttachmentDto
        {
            Id = request.AttachmentId,
            SyncToken = syncToken
        };

        await Client.ExecuteWithJson($"/attachable?operation=delete", Method.Post, body, Creds);
    }

    [Action("Download attachment", Description = "Download attachment by ID.")]
    public async Task<DownloadAttachmentResponse> DownloadAttachableById([ActionParameter] AttachmentRequest request)
    {
        var attachable =
            await Client.ExecuteWithJson<AttachableWrapper>($"/attachable/{request.AttachmentId}", Method.Get, null,
                Creds);
        var downloadUrl = attachable.Attachable.TempDownloadUri;

        if (string.IsNullOrEmpty(downloadUrl))
        {
            throw new Exception("You can not download this attachment, file is missing.");
        }

        var restClient = new RestClient(downloadUrl!);
        var restRequest = new QuickBooksRequest(new QuickBookRequestParameters
        {
            Method = Method.Get,
            Url = string.Empty
        }, Creds);
        var restResponse = await restClient.ExecuteAsync(restRequest);

        var memoryStream = new MemoryStream(restResponse.RawBytes!);
        memoryStream.Position = 0;

        var attachment = await GetAttachableById(request);
        string fileName = attachment.FileName ??
                          restResponse.Headers?.FirstOrDefault(x => x.Name == "Content-Disposition")?.Value?.ToString()
                              ?.Split("filename=")[1] ?? "attachment";

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

        var attachable = await GetAttachableById(request);
        return attachable.SyncToken;
    }
}