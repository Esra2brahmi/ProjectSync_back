using Microsoft.AspNetCore.Mvc;
using projectSync_back.Dtos.Supervisor;
using projectSync_back.Interfaces;
using projectSync_back.Mappers;
using projectSync_back.Models;

namespace projectSync_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupervisorController : ControllerBase
    {
        private readonly ISupervisorRepository _supervisorRepo;

        public SupervisorController(ISupervisorRepository supervisorRepo)
        {
            _supervisorRepo = supervisorRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var supervisors = await _supervisorRepo.GetAllAsync();
            var supervisorDtos = supervisors.Select(s => s.ToSupervisorDto());
            return Ok(supervisorDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var supervisor = await _supervisorRepo.GetByIdAsync(id);
            if (supervisor == null)
            {
                return NotFound();
            }
            return Ok(supervisor.ToSupervisorDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupervisorDto supervisorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map DTO to model
            var supervisorModel = supervisorDto.ToSupervisorFromCreateDto();
            
            // Create supervisor with department associations
            var createdSupervisor = await _supervisorRepo.CreateAsync(
                supervisorModel, 
                supervisorDto.DepartmentIds
            );

            return CreatedAtAction(
                nameof(GetById), 
                new { id = createdSupervisor.Id }, 
                createdSupervisor.ToSupervisorDto()
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSupervisorDto updateDto)
        {
            var supervisorModel = await _supervisorRepo.GetByIdAsync(id);
            if (supervisorModel == null)
            {
                return NotFound();
            }

            supervisorModel.FirstName = updateDto.FirstName;
            supervisorModel.LastName = updateDto.LastName;
            supervisorModel.Email = updateDto.Email;
            supervisorModel.PhoneNumber = updateDto.PhoneNumber;
            supervisorModel.Description = updateDto.Description;
            supervisorModel.Address = updateDto.Address;
             supervisorModel.AcademicTitle= updateDto.AcademicTitle;

            await _supervisorRepo.UpdateAsync(id, supervisorModel);
            return Ok(supervisorModel.ToSupervisorDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var supervisorModel = await _supervisorRepo.DeleteAsync(id);
            if (supervisorModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}