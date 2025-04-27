using Microsoft.AspNetCore.Mvc;
using projectSync_back.Dtos.JuryMember;
using projectSync_back.Interfaces;
using projectSync_back.Mappers;
using projectSync_back.Models;

namespace projectSync_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JuryMemberController : ControllerBase
    {
        private readonly IJuryMemberRepository _juryMemberRepo;

        public JuryMemberController(IJuryMemberRepository juryMemberRepo)
        {
            _juryMemberRepo = juryMemberRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var juryMembers = await _juryMemberRepo.GetAllAsync();
            var juryMemberDtos = juryMembers.Select(s => s.ToJuryMemberDto());
            return Ok(juryMemberDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var juryMember = await _juryMemberRepo.GetByIdAsync(id);
            if (juryMember == null)
            {
                return NotFound();
            }
            return Ok(juryMember.ToJuryMemberDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJuryMemberDto juryMemberDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map DTO to model
            var juryMemberModel = juryMemberDto.ToJuryMemberFromCreateDto();
            
            // Create JuryMember with department associations
            var createdJuryMember = await _juryMemberRepo.CreateAsync(
                juryMemberModel, 
                juryMemberDto.DepartmentIds
            );

            return CreatedAtAction(
                nameof(GetById), 
                new { id = createdJuryMember.Id }, 
                createdJuryMember.ToJuryMemberDto()
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateJuryMemberDto updateDto)
        {
            var juryMemberModel = await _juryMemberRepo.GetByIdAsync(id);
            if (juryMemberModel == null)
            {
                return NotFound();
            }

            juryMemberModel.FirstName = updateDto.FirstName;
            juryMemberModel.LastName = updateDto.LastName;
            juryMemberModel.Email = updateDto.Email;
            juryMemberModel.PhoneNumber = updateDto.PhoneNumber;
            juryMemberModel.Description = updateDto.Description;
            juryMemberModel.Address = updateDto.Address;
            juryMemberModel.AcademicTitle= updateDto.AcademicTitle;

            await _juryMemberRepo.UpdateAsync(id, juryMemberModel);
            return Ok(juryMemberModel.ToJuryMemberDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var juryMemberModel = await _juryMemberRepo.DeleteAsync(id);
            if (juryMemberModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}