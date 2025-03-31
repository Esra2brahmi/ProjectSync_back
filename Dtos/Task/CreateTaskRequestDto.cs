using System;
using System.ComponentModel.DataAnnotations;

namespace projectSync_back.Dtos.Task
{
    public class CreateTaskRequestDto
    {
        public string TaskName { get; set; } =string.Empty;
        public string TaskDescription { get; set; } =string.Empty;
        public DateTime DueDate { get; set; }

        internal object ToTaskFromCreateDto()
        {
            throw new NotImplementedException();
        }
    }
}
