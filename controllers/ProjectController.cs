using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectSync_back.data;
using projectSync_back.Dtos.Project;
using projectSync_back.Models;
using projectSync_back.Mappers;
using Microsoft.EntityFrameworkCore;
using projectSync_back.Interfaces;

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

        private readonly IProjectRepository _projectRepo;
        public ProjectController(ApplicationDBContext context,IProjectRepository projectRepo)
        {
           _projectRepo=projectRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var projects = await _projectRepo.GetAllAsync();
            var projectDto=projects.Select(s=>s.ToProjectDto());
            return Ok(projects);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var project =await _projectRepo.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project.ToProjectDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequestDto projectDto)
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
            await _projectRepo.CreateAsync(projectModel);
            
            // Return the created project with its ID
            return CreatedAtAction(nameof(GetById), new { id = projectModel.Id }, projectModel);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProjectRequestDto updateDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var projectModel= await _projectRepo.UpdateAsync(id,updateDto);
            if(projectModel == null){
                return NotFound();
            }

            return Ok(projectModel.ToProjectDto());

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var projectModel=await _projectRepo.DeleteAsync(id);

            if(projectModel==null){
                return NotFound();
            }
            return NoContent();
        }

    }
}
