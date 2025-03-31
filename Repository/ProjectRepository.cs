using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projectSync_back.data;
using projectSync_back.Dtos.Project;
using projectSync_back.Interfaces;
using projectSync_back.Models;

namespace projectSync_back.Repository
{
    public class ProjectRepository:IProjectRepository
    {
        private readonly ApplicationDBContext _context;
        public ProjectRepository(ApplicationDBContext context){
            _context=context;
        }

        public async Task<Project> CreateAsync(Project projectModel)
        {
            await _context.Projects.AddAsync(projectModel);
            await _context.SaveChangesAsync();
            return projectModel;
        }

        public async Task<Project?> DeleteAsync(int id)
        {
            var projectModel=await _context.Projects.FirstOrDefaultAsync(x=>x.Id==id);

            if(projectModel==null){
                return null;
            }

            _context.Projects.Remove(projectModel);
            await _context.SaveChangesAsync();
            return projectModel;
        }

        public async Task<List<Project>> GetAllAsync(){
            return await  _context.Projects.Include(c=>c.ProjectTasks).ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects.Include(c=>c.ProjectTasks).FirstOrDefaultAsync(i=>i.Id==id);
        }

        public Task<bool> ProjectExists(int id)
        {
            return _context.Projects.AnyAsync(s=>s.Id==id);
        }

        public async Task<Project?> UpdateAsync(int id, UpdateProjectRequestDto projectDto)
        {
            var existingProject=await _context.Projects.FirstOrDefaultAsync(x=>x.Id==id);

            if(existingProject==null){
                return null;
            }

            existingProject.ProjectName=projectDto.ProjectName;
            existingProject.SupervisorFirstName=projectDto.SupervisorFirstName;
            existingProject.SupervisorLastName=projectDto.SupervisorLastName;
            existingProject.EndDate=projectDto.EndDate;
            existingProject.StartDate=projectDto.StartDate;
            existingProject.Status=projectDto.Status;

            await _context.SaveChangesAsync();

            return existingProject;
        }
    }
}