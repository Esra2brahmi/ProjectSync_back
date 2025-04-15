using Microsoft.EntityFrameworkCore;
using projectSync_back.data;
using projectSync_back.Interfaces;
using projectSync_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectSync_back.Dtos.Supervisor;



namespace projectSync_back.Repository
{
    public class SupervisorRepository : ISupervisorRepository
    {
        private readonly ApplicationDBContext _context;

        public SupervisorRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Supervisor>> GetAllAsync()
        {
            return await _context.Supervisors
            .Include(s => s.DepSupervisors)  // Include the join table
            .ToListAsync();
        }

        public async Task<Supervisor?> GetByIdAsync(int id)
        {
            return await _context.Supervisors.FindAsync(id);
        }

        public async Task<Supervisor> CreateAsync(Supervisor supervisorModel, List<int> departmentIds)
{
    using var transaction = await _context.Database.BeginTransactionAsync();
    
    try
    {
        // 1. Add supervisor first
        await _context.Supervisors.AddAsync(supervisorModel);
        await _context.SaveChangesAsync();

        // 2. Process department associations
        if (departmentIds != null && departmentIds.Any())
        {
            // Get existing associations to avoid duplicates
            var existingDeptIds = await _context.DepSupervisors
                .Where(ds => ds.SupervisorId == supervisorModel.Id)
                .Select(ds => ds.DepartmentId)
                .ToListAsync();

            // Only add new associations that don't already exist
            var newDeptIds = departmentIds.Except(existingDeptIds);
            
            foreach (var departmentId in newDeptIds)
            {
                _context.DepSupervisors.Add(new DepSupervisor
                {
                    SupervisorId = supervisorModel.Id,
                    DepartmentId = departmentId
                });
            }
            
            if (newDeptIds.Any())
            {
                await _context.SaveChangesAsync();
            }
        }

        await transaction.CommitAsync();
        return supervisorModel;
    }
    catch
    {
        await transaction.RollbackAsync();
        throw;
    }
}

        public async Task<Supervisor?> UpdateAsync(int id, Supervisor supervisorModel)
        {
            var existingSupervisor = await _context.Supervisors.FindAsync(id);
            if (existingSupervisor == null)
            {
                return null;
            }

            existingSupervisor.FirstName = supervisorModel.FirstName;
            existingSupervisor.LastName = supervisorModel.LastName;
            existingSupervisor.Email = supervisorModel.Email;
            existingSupervisor.PhoneNumber = supervisorModel.PhoneNumber;
            existingSupervisor.Description = supervisorModel.Description;
            existingSupervisor.Address = supervisorModel.Address;
            existingSupervisor.AcademicTitle= supervisorModel.AcademicTitle;

            await _context.SaveChangesAsync();
            return existingSupervisor;
        }

        public async Task<Supervisor?> DeleteAsync(int id)
        {
            var supervisorModel = await _context.Supervisors.FindAsync(id);
            if (supervisorModel == null)
            {
                return null;
            }

            _context.Supervisors.Remove(supervisorModel);
            await _context.SaveChangesAsync();
            return supervisorModel;
        }

        public async Task<bool> SupervisorExists(int id)
        {
            return await _context.Supervisors.AnyAsync(s => s.Id == id);
        }
    }
}