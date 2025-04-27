using Microsoft.AspNetCore.Mvc;
using projectSync_back.Dtos.ProjectUser;
using projectSync_back.Interfaces;
using projectSync_back.Mappers;
using System.Threading.Tasks;
using projectSync_back.Dtos.User; 

namespace projectSync_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectUserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;

        public ProjectUserController(
            IUserRepository userRepository,
            IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }

        [HttpPut("assign")]
        public async Task<ActionResult> AssignUserToProject([FromBody] ProjectUserAssignmentDto dto)
        {
            // Check if user is already assigned to this project
            if (await _userRepository.IsUserAssignedToProjectAsync(dto.UserId, dto.ProjectId))
            {
                return BadRequest("User is already assigned to this project");
            }

            // Assign user to project
            var result = await _userRepository.AssignUserToProjectAsync(dto.UserId, dto.ProjectId);
            if (!result)
            {
                return NotFound("User or project not found");
            }

            return Ok();
        }

        [HttpPut("remove")]
        public async Task<ActionResult> RemoveUserFromProject([FromBody] ProjectUserAssignmentDto dto)
        {
            // Check if user is assigned to this project
            if (!await _userRepository.IsUserAssignedToProjectAsync(dto.UserId, dto.ProjectId))
            {
                return BadRequest("User is not assigned to this project");
            }

            var result = await _userRepository.RemoveUserFromProjectAsync(dto.UserId, dto.ProjectId);
            if (!result)
            {
                return NotFound("User or project not found");
            }

            return Ok();
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByProject(int projectId)
        {
            var users = await _userRepository.GetUsersByProjectIdAsync(projectId);
            if (users == null)
            {
                return NotFound("Project not found");
            }

            // Assuming you have a ToDto mapper for User
            return Ok(users.Select(u => u.ToUserDto()));
        }
    }
}