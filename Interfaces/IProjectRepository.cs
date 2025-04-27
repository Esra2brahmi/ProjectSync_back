using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectSync_back.Dtos.Project;
using projectSync_back.Models;

namespace projectSync_back.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task<Project> CreateAsync(Project projectModel);
        Task<Project?> UpdateAsync(int id,UpdateProjectRequestDto projectDto);
        Task<Project?> DeleteAsync(int id);
        Task<bool> ProjectExists(int id);
        Task<bool> AddUserToProjectAsync(int projectId, User user);
        Task<bool> RemoveUserFromProjectAsync(int projectId, int userId);
    }
}