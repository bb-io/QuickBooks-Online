﻿using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Dtos.Attachables;

public class DeleteAttachmentDto
{
    [Display("Id")]
    public string Id { get; set; }
    
    [Display("SyncToken")]   
    public string SyncToken { get; set; }
}