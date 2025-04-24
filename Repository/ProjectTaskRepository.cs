using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projectSync_back.data;
using projectSync_back.Interfaces;
using projectSync_back.Models;
using projectSync_back.Dtos.Task;

namespace projectSync_back.Repository
{
    public class ProjectTaskRepository : ITaskRepository
    {
        private readonly ApplicationDBContext _context;
        public ProjectTaskRepository(ApplicationDBContext context)
        {
            _context=context;
        }

        public async Task<ProjectTask> CreateAsync(ProjectTask taskModel)
        {
            await _context.Tasks.AddAsync(taskModel);
            await _context.SaveChangesAsync();
            return taskModel;
        }

        public async Task<ProjectTask?> DeleteAsync(int id)
        {
            var taskModel=await _context.Tasks.FirstOrDefaultAsync(x=>x.Id == id);
            if(taskModel==null){
                return null;
            }

            _context.Tasks.Remove(taskModel);
            await _context.SaveChangesAsync();
            return taskModel;
        }

        public async Task<List<ProjectTask>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<ProjectTask?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<List<ProjectTask>> GetByProjectIdAsync(int projectId)
        {
             return await _context.Tasks
                                  .Where(t => t.ProjectId == projectId)
                                  .ToListAsync();
        }

        public async Task<ProjectTask?> UpdateAsync(int id, UpdateTaskRequestDto taskDto)
        {
             var existingTask=await _context.Tasks.FirstOrDefaultAsync(x=>x.Id==id);
            if(existingTask==null){
                return null;
            }
            existingTask.TaskName=taskDto.TaskName;
            existingTask.TaskDescription=taskDto.TaskDescription;
            existingTask.DueDate = taskDto.DueDate.ToUniversalTime();

            await _context.SaveChangesAsync();

            return existingTask;
        }

        public async Task<ProjectTask> GetTaskByIdAsync(int taskId)
    {
        return await _context.Tasks
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == taskId);
    }

    public async Task<bool> IsTaskAssignedAsync(int taskId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId);
        return task?.UserId != null;
    }

    public async Task<bool> AssignTaskToUserAsync(int taskId, int userId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId);

        if (task == null)
            return false;

        task.UserId = userId;
        return true;
    }
    }
}