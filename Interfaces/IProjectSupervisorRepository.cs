using projectSync_back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace projectSync_back.Interfaces
{
    public interface IProjectSupervisorRepository
    {
        Task<ProjectSupervisor> GetProjectSupervisorAsync(int projectId, int supervisorId);
        Task<IEnumerable<ProjectSupervisor>> GetProjectSupervisorsByProjectAsync(int projectId);
        Task<IEnumerable<ProjectSupervisor>> GetProjectSupervisorsBySupervisorAsync(int supervisorId);
        Task<ProjectSupervisor> AddProjectSupervisorAsync(ProjectSupervisor projectSupervisor);
        Task<bool> DeleteProjectSupervisorAsync(int projectId, int supervisorId);
        Task<bool> ExistsAsync(int projectId, int supervisorId);
    }
}