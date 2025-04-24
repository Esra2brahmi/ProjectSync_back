using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projectSync_back.Dtos.Task;
using projectSync_back.Models;

namespace projectSync_back.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<ProjectTask>> GetAllAsync();
        Task<ProjectTask?> GetByIdAsync(int id);
        Task<List<ProjectTask>>GetByProjectIdAsync(int projectId);
        Task<ProjectTask> CreateAsync(ProjectTask taskModel);
        Task<ProjectTask?> UpdateAsync(int id,UpdateTaskRequestDto taskDto);
        Task<ProjectTask?> DeleteAsync(int id);
        Task<bool> AssignTaskToUserAsync(int taskId, int userId);
        Task<ProjectTask> GetTaskByIdAsync(int taskId);
        Task<bool> IsTaskAssignedAsync(int taskId);
    }
}