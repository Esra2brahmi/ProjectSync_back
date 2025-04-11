using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projectSync_back.data;
using projectSync_back.Dtos.Attachment;
using projectSync_back.Interfaces;
using projectSync_back.Models;


namespace projectSync_back.Repository
{
    public class AttachmentRepository : IAttachmentRepository
{
    private readonly ApplicationDBContext _context;
    private readonly IWebHostEnvironment _environment;

    public AttachmentRepository(ApplicationDBContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<Attachment> UploadAttachmentAsync(CreateAttachmentDto attachmentDto)
    {
        var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsPath))
            Directory.CreateDirectory(uploadsPath);

        var storedFileName = $"{Guid.NewGuid()}{Path.GetExtension(attachmentDto.File.FileName)}";
        var filePath = Path.Combine(uploadsPath, storedFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await attachmentDto.File.CopyToAsync(stream);
        }

        var attachment = new Attachment
        {
            FileName = attachmentDto.File.FileName,
            StoredFileName = storedFileName,
            ContentType = attachmentDto.File.ContentType,
            FileSize = attachmentDto.File.Length,
            UploadDate = DateTime.UtcNow,
            TaskId = attachmentDto.TaskId
        };

        _context.Attachments.Add(attachment);
        await _context.SaveChangesAsync();

        return attachment;
    }

    public async Task<IEnumerable<Attachment>> GetAttachmentsByTaskIdAsync(int taskId)
    {
        return await _context.Attachments
            .Where(a => a.TaskId == taskId)
            .ToListAsync();
    }

    public async Task<Attachment> GetAttachmentByIdAsync(int id)
    {
        return await _context.Attachments.FindAsync(id);
    }

    public async Task<bool> DeleteAttachmentAsync(int id)
    {
        var attachment = await _context.Attachments.FindAsync(id);
        if (attachment == null) return false;

        var filePath = Path.Combine(_environment.WebRootPath, "uploads", attachment.StoredFileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        _context.Attachments.Remove(attachment);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<string> GetFilePathAsync(int attachmentId)
    {
        var attachment = await _context.Attachments.FindAsync(attachmentId);
        if (attachment == null) return null;

        return Path.Combine(_environment.WebRootPath, "uploads", attachment.StoredFileName);
    }
}
}