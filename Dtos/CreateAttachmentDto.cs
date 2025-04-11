using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectSync_back.Dtos.Task;

namespace projectSync_back.Dtos.Attachement
{
public class CreateAttachmentDto
{
    public IFormFile File { get; set; }
    public int TaskId { get; set; }
}
}