using Microsoft.AspNetCore.Mvc;
using projectSync_back.Dtos.ProjectSupervisor;
using projectSync_back.Interfaces;
using projectSync_back.Mappers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace projectSync_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectSupervisorController : ControllerBase
    {
        private readonly IProjectSupervisorRepository _projectSupervisorRepository;

        public ProjectSupervisorController(IProjectSupervisorRepository projectSupervisorRepository)
        {
            _projectSupervisorRepository = projectSupervisorRepository;
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectSupervisorResponseDto>>> GetByProject(int projectId)
        {
            var projectSupervisors = await _projectSupervisorRepository.GetProjectSupervisorsByProjectAsync(projectId);
            return Ok(projectSupervisors.Select(ps => ps.ToResponseDto()));
        }

        [HttpGet("supervisor/{supervisorId}")]
        public async Task<ActionResult<IEnumerable<ProjectSupervisorResponseDto>>> GetBySupervisor(int supervisorId)
        {
            var projectSupervisors = await _projectSupervisorRepository.GetProjectSupervisorsBySupervisorAsync(supervisorId);
            return Ok(projectSupervisors.Select(ps => ps.ToResponseDto()));
        }

        [HttpPost]
        public async Task<ActionResult<ProjectSupervisorResponseDto>> AddProjectSupervisor(ProjectSupervisorDto projectSupervisorDto)
        {
            if (await _projectSupervisorRepository.ExistsAsync(projectSupervisorDto.ProjectId, projectSupervisorDto.SupervisorId))
                return BadRequest("This supervisor is already assigned to this project");

            var projectSupervisor = projectSupervisorDto.ToEntity();
            var result = await _projectSupervisorRepository.AddProjectSupervisorAsync(projectSupervisor);
            
            return CreatedAtAction(
                nameof(GetByProject),
                new { projectId = result.ProjectId },
                result.ToResponseDto()
            );
        }

        [HttpDelete("{projectId}/{supervisorId}")]
        public async Task<ActionResult> DeleteProjectSupervisor(int projectId, int supervisorId)
        {
            var result = await _projectSupervisorRepository.DeleteProjectSupervisorAsync(projectId, supervisorId);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}