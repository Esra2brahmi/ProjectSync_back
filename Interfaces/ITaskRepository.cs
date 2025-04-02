using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projectSync_back.Models;

namespace projectSync_back.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<ProjectTask>> GetAllAsync();
        Task<ProjectTask?> GetByIdAsync(int id);
        Task<List<ProjectTask>>GetByProjectIdAsync(int projectId);
        Task<ProjectTask> CreateAsync(ProjectTask taskModel);
        Task<ProjectTask?> UpdateAsync(int id,ProjectTask taskModel);
        Task<ProjectTask?> DeleteAsync(int id);
    }
}