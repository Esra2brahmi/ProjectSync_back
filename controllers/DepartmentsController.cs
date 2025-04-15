using Microsoft.AspNetCore.Mvc;
using projectSync_back.Dtos.Department;
using projectSync_back.Interfaces;
using projectSync_back.Models;

namespace projectSync_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        // GET: api/departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAll()
        {
            var departments = await _departmentRepository.GetAllAsync();
            var departmentDtos = departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                ChairName = d.ChairName,
                PhoneNumber = d.PhoneNumber,
                Email = d.Email
            });
            
            return Ok(departmentDtos);
        }

        // GET: api/departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetById(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null) return NotFound();
            
            var departmentDto = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                ChairName = department.ChairName,
                PhoneNumber = department.PhoneNumber,
                Email = department.Email
            };
            
            return Ok(departmentDto);
        }

        // POST: api/departments
        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> Create([FromBody] CreateDepartmentDto createDto)
        {
            var department = new Department
            {
                Name = createDto.Name,
                ChairName = createDto.ChairName,
                PhoneNumber = createDto.PhoneNumber,
                Email = createDto.Email
            };

            var createdDept = await _departmentRepository.CreateAsync(department);
            
            var readDto = new DepartmentDto
            {
                Id = createdDept.Id,
                Name = createdDept.Name,
                ChairName = createdDept.ChairName,
                PhoneNumber = createdDept.PhoneNumber,
                Email = createdDept.Email
            };

            return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
        }

        // PUT: api/departments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentDto updateDto)
        {
            if (id != updateDto.Id) return BadRequest("ID mismatch");

            if (!await _departmentRepository.Exists(id))
                return NotFound();

            var department = new Department
            {
                Id = updateDto.Id,
                Name = updateDto.Name,
                ChairName = updateDto.ChairName,
                PhoneNumber = updateDto.PhoneNumber,
                Email = updateDto.Email
            };

            var updatedDept = await _departmentRepository.UpdateAsync(id, department);

            if (updatedDept == null) return NotFound();

            return NoContent();
        }

        // DELETE: api/departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _departmentRepository.DeleteAsync(id);
            if (!success) return NotFound();
            
            return NoContent();
        }
    }
}