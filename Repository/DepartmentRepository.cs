using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectSync_back.data;
using projectSync_back.Dtos.Department;
using projectSync_back.Interfaces;
using projectSync_back.Models;

namespace projectSync_back.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDBContext _context;

        public DepartmentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Department> CreateAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department?> UpdateAsync(int id, Department department)
        {
            var existingDept = await GetByIdAsync(id);
            if (existingDept == null) return null;

            existingDept.Name = department.Name;
            existingDept.ChairName = department.ChairName;
            existingDept.PhoneNumber = department.PhoneNumber;
            existingDept.Email = department.Email;

            await _context.SaveChangesAsync();
            return existingDept;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = await GetByIdAsync(id);
            if (department == null) return false;

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Departments.AnyAsync(d => d.Id == id);
        }
    }
}