using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using projectSync_back.data;
using projectSync_back.Dtos.Task;
using projectSync_back.Dtos.User;
using projectSync_back.Models;
using projectSync_back.Mappers;
using Microsoft.EntityFrameworkCore;
using projectSync_back.Interfaces;

namespace projectSync_back.Controllers
{
    [Route("/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IUserRepository _userRepository;
        public TaskController(ITaskRepository taskRepo,IProjectRepository projectRepo,IUserRepository userRepository)
        {
            _taskRepo = taskRepo;
            _projectRepo=projectRepo;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var tasks = await _taskRepo.GetAllAsync();
            var taskDto=tasks.Select(s=>s.ToTaskDto());
            return Ok(taskDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var task = await _taskRepo.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task.ToTaskDto());
        }

        [HttpGet("byProject/{projectId}")]
        public async Task<IActionResult> GetByProjectId([FromRoute] int projectId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tasks = await _taskRepo.GetByProjectIdAsync(projectId);
            
            if (tasks == null || tasks.Count == 0)
            {
                return NotFound();
            }

            return Ok(tasks.Select(t => t.ToTaskDto()));
        }


        [HttpPost("{projectId:int}")]
        public async Task<IActionResult> Create([FromRoute] int projectId,CreateTaskRequestDto taskDto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            if(!await _projectRepo.ProjectExists(projectId)){
                return BadRequest("Project does not exist");
            }
            

            taskDto.DueDate = taskDto.DueDate.ToUniversalTime();

            var taskModel = taskDto.ToTaskFromCreateDto(projectId);
            await _taskRepo.CreateAsync(taskModel);
            
            return CreatedAtAction(nameof(GetById), new { id = taskModel.Id }, taskModel.ToTaskDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTaskRequestDto updateDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var taskModel=await _taskRepo.UpdateAsync(id,updateDto);
            if(taskModel == null){
                return NotFound("task not found");
            }

            return Ok(taskModel.ToTaskDto());

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var taskModel=await _taskRepo.DeleteAsync(id);

            if(taskModel==null){
                return NotFound("task does not exist");
            }

            return NoContent();
        }

        [HttpPut("assign-student")]
    public async Task<ActionResult> AssignTaskToStudent([FromBody] AssignTaskToUserDto dto)
    {
        // Check if task exists and is not already assigned
        var task = await _taskRepo.GetTaskByIdAsync(dto.TaskId);
        if (task == null)
            return NotFound("Task not found");

        if (await _taskRepo.IsTaskAssignedAsync(dto.TaskId))
            return BadRequest("Task is already assigned to a student");

        // Check if user exists
        var user = await _userRepository.GetUserByIdAsync(dto.UserId);
        if (user == null)
            return NotFound("Student not found");

        // Assign task to user
        var assignResult = await _taskRepo.AssignTaskToUserAsync(dto.TaskId, dto.UserId);
        if (!assignResult)
            return BadRequest("Failed to assign task to student");

        // Add task to user's task list
        var addToUserResult = await _userRepository.AddTaskToUserAsync(dto.UserId, task);
        if (!addToUserResult)
            return BadRequest("Failed to add task to student's task list");

        return Ok();
    }

    [HttpGet("{id:int}/student")] 
        public async Task<ActionResult<UserDto>> GetStudentForTask([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the task, including the related User
            // GetTaskByIdAsync already includes the User thanks to the .Include in the repo
            var task = await _taskRepo.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound("Task not found");
            }

            // Check if a student (User) is actually assigned
            if (task.User == null)
            {
                return NotFound("No student assigned to this task");
            }

            var userDto = task.User.ToUserDto(); 

            return Ok(userDto);
        }




        
    }
}
