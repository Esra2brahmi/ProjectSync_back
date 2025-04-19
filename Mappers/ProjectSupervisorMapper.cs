using projectSync_back.Models;
using projectSync_back.Dtos.ProjectSupervisor;

namespace projectSync_back.Mappers
{
    public static class ProjectSupervisorMapper
    {
        public static ProjectSupervisor ToEntity(this ProjectSupervisorDto dto)
        {
            return new ProjectSupervisor
            {
                ProjectId = dto.ProjectId,
                SupervisorId = dto.SupervisorId
            };
        }

        public static ProjectSupervisorResponseDto ToResponseDto(this ProjectSupervisor projectSupervisor)
        {
            return new ProjectSupervisorResponseDto
            {
                ProjectId = projectSupervisor.ProjectId,
                SupervisorId = projectSupervisor.SupervisorId,
                SupervisorName = projectSupervisor.Supervisor?.FirstName + " " + projectSupervisor.Supervisor?.LastName,
                ProjectTitle = projectSupervisor.Project?.ProjectName
            };
        }
    }
}