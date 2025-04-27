using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectSync_back.Dtos.Task;

namespace projectSync_back.Dtos.Project
{
    public class ProjectDto
    {
        public int Id { get; set; } 
        public string ProjectName { get; set; } =string.Empty;
        public string SupervisorFirstName { get; set; } =string.Empty;
        public string SupervisorLastName { get; set; } =string.Empty;
        public string Department { get; set; } =string.Empty;
        public string Level { get; set; } =string.Empty;
        public string ProjectReference { get; set; } = string.Empty;
        public string Status { get; set; } =string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<TaskDto> Tasks {get;set;}
    }
}