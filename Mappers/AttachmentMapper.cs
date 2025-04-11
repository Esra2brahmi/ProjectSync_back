using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectSync_back.Dtos.Attachment;
using projectSync_back.Models;
using Microsoft.AspNetCore.Mvc; 

namespace projectSync_back.Mappers
{
    public static class AttachmentMappers
{
    public static AttachmentDto ToDto(this Attachment attachment, IUrlHelper urlHelper)
    {
        return new AttachmentDto
        {
            Id = attachment.Id,
            FileName = attachment.FileName,
            FileUrl = urlHelper.Action("Download", "Attachment", new { id = attachment.Id }),
            ContentType = attachment.ContentType,
            FileSize = attachment.FileSize,
            UploadDate = attachment.UploadDate,
            TaskId = attachment.TaskId
        };
    }
}
}