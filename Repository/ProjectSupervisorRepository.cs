using Microsoft.EntityFrameworkCore;
using projectSync_back.data;
using projectSync_back.Interfaces;
using projectSync_back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace projectSync_back.Repository
{
    public class ProjectSupervisorRepository : IProjectSupervisorRepository
    {
        private readonly ApplicationDBContext _context;

        public ProjectSupervisorRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ProjectSupervisor> GetProjectSupervisorAsync(int projectId, int supervisorId)
        {
            return await _context.ProjectSupervisors
                .Include(ps => ps.Project)
                .Include(ps => ps.Supervisor)
                .FirstOrDefaultAsync(ps => ps.ProjectId == projectId && ps.SupervisorId == supervisorId);
        }

        public async Task<IEnumerable<ProjectSupervisor>> GetProjectSupervisorsByProjectAsync(int projectId)
        {
            return await _context.ProjectSupervisors
                .Include(ps => ps.Supervisor)
                .Where(ps => ps.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProjectSupervisor>> GetProjectSupervisorsBySupervisorAsync(int supervisorId)
        {
            return await _context.ProjectSupervisors
                .Include(ps => ps.Project)
                .Where(ps => ps.SupervisorId == supervisorId)
                .ToListAsync();
        }

        public async Task<ProjectSupervisor> AddProjectSupervisorAsync(ProjectSupervisor projectSupervisor)
        {
            await _context.ProjectSupervisors.AddAsync(projectSupervisor);
            await _context.SaveChangesAsync();
            return projectSupervisor;
        }

        public async Task<bool> DeleteProjectSupervisorAsync(int projectId, int supervisorId)
        {
            var projectSupervisor = await _context.ProjectSupervisors
                .FirstOrDefaultAsync(ps => ps.ProjectId == projectId && ps.SupervisorId == supervisorId);
            
            if (projectSupervisor == null)
                return false;

            _context.ProjectSupervisors.Remove(projectSupervisor);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(int projectId, int supervisorId)
        {
            return await _context.ProjectSupervisors
                .AnyAsync(ps => ps.ProjectId == projectId && ps.SupervisorId == supervisorId);
        }
    }
}