using System;
using System.ComponentModel.DataAnnotations; 

namespace projectSync_back.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string StoredFileName { get; set; }  // GUID-based name for storage
        public string ContentType { get; set; }     // MIME type (e.g., "image/png")
        public long FileSize { get; set; }          // In bytes
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        
        // Foreign key to Task
        public int? TaskId { get; set; }
        public ProjectTask? Task { get; set; }              // Navigation property

   }
}
