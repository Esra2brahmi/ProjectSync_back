using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectSync_back.Dtos.Task;

namespace projectSync_back.Dtos.Attachement
{
    public class AttachmentDto
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FileUrl { get; set; } // URL to download the file
    public string ContentType { get; set; }
    public long FileSize { get; set; }
    public DateTime UploadDate { get; set; }
    public int? TaskId { get; set; }
}
}