using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectSync_back.Dtos.Attachment;
using projectSync_back.Models;

namespace projectSync_back.Interfaces
{
    public interface IAttachmentRepository
{
    Task<Attachment> UploadAttachmentAsync(CreateAttachmentDto attachmentDto);
    Task<IEnumerable<Attachment>> GetAttachmentsByTaskIdAsync(int taskId);
    Task<Attachment> GetAttachmentByIdAsync(int id);
    Task<bool> DeleteAttachmentAsync(int id);
    Task<string> GetFilePathAsync(int attachmentId);
}
}