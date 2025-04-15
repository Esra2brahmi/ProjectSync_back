using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectSync_back.Dtos.Department;
using projectSync_back.Models;

namespace projectSync_back.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task<Department> CreateAsync(Department department);
        Task<Department?> UpdateAsync(int id, Department department);
        Task<bool> DeleteAsync(int id);
        Task<bool> Exists(int id);
    }
}