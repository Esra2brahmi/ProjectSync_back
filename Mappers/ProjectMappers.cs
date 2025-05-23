using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectSync_back.Dtos.Project;
using projectSync_back.Models;

namespace projectSync_back.Mappers
{
    public static class ProjectMappers
    {
        public static ProjectDto ToProjectDto(this Project projectModel){
            return new ProjectDto{
                Id = projectModel.Id,
                ProjectName =projectModel.ProjectName,
                SupervisorFirstName = projectModel.SupervisorFirstName,
                SupervisorLastName = projectModel.SupervisorLastName,
                Status =projectModel.Status,
                Department =projectModel.Department,
                Level =projectModel.Level,
                StartDate =projectModel.StartDate,
                EndDate =projectModel.EndDate,
                ProjectReference =projectModel.ProjectReference,
                Tasks=projectModel.ProjectTasks.Select(c=>c.ToTaskDto()).ToList()
            };
            
        }
        public static Project ToProjectFromCreateDto(this CreateProjectRequestDto projectDto){
            return new Project{
                ProjectName =projectDto.ProjectName,
                SupervisorFirstName = projectDto.SupervisorFirstName,
                SupervisorLastName = projectDto.SupervisorLastName,
                Status =projectDto.Status,
                Department =projectDto.Department,
                Level =projectDto.Level,
                StartDate =projectDto.StartDate,
                EndDate =projectDto.EndDate,
                ProjectReference =projectDto.ProjectReference
            };
            
        }
    }
}