using System;

namespace projectSync_back.Dtos.ProjectSupervisor
{
    public class ProjectSupervisorDto
    {
        public int ProjectId { get; set; }
        public int SupervisorId { get; set; }
    }

    public class ProjectSupervisorResponseDto
    {
        public int ProjectId { get; set; }
        public int SupervisorId { get; set; }
        public string SupervisorName { get; set; }
        public string ProjectTitle { get; set; }
    }
}