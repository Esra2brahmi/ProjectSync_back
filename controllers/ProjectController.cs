using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectSync_back.data;
using projectSync_back.Dtos.Project;
using projectSync_back.Models;
using projectSync_back.Mappers;

namespace projectSync_back.Controllers
{
    [Route("/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        [HttpOptions]
        [Route("project")]
        public IActionResult Preflight()
        {
            return NoContent(); 
        }

        private readonly ApplicationDBContext _context;
        public ProjectController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var projects = _context.Projects.ToList();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProjectRequestDto projectDto)
        {
            // Validate the DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convert the dates to UTC before mapping to the entity
            projectDto.StartDate = projectDto.StartDate.ToUniversalTime();
            projectDto.EndDate = projectDto.EndDate.ToUniversalTime();

            var projectModel = projectDto.ToProjectFromCreateDto();
            _context.Projects.Add(projectModel);
            _context.SaveChanges();
            
            // Return the created project with its ID
            return CreatedAtAction(nameof(GetById), new { id = projectModel.Id }, projectModel);
        }
    }
}
