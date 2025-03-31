using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projectSync_back.data;
using projectSync_back.Interfaces;
using projectSync_back.Models;

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

        public async Task<ProjectTask?> UpdateAsync(int id, ProjectTask taskModel)
        {
            var existingTask=await _context.Tasks.FindAsync(id);
            if(existingTask==null){
                return null;
            }
            existingTask.TaskName=taskModel.TaskName;
            existingTask.TaskDescription=taskModel.TaskDescription;
            existingTask.DueDate=taskModel.DueDate;

            await _context.SaveChangesAsync();

            return existingTask;

        }
    }
}