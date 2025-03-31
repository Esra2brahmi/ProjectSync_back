using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectSync_back.Dtos.Task
{
    public class UpdateTaskRequestDto
    {
        public string TaskName { get; set; } =string.Empty;
        public string TaskDescription { get; set; } =string.Empty;
        public DateTime DueDate { get; set; }
    }
}