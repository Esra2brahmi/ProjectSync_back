using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectSync_back.Dtos.Supervisor;
using projectSync_back.Models;



namespace projectSync_back.Interfaces
{
    public interface ISupervisorRepository
    {
        Task<List<Supervisor>> GetAllAsync();
        Task<Supervisor?> GetByIdAsync(int id);
        Task<Supervisor> CreateAsync(Supervisor supervisorModel, List<int> departmentIds);
        Task<Supervisor?> UpdateAsync(int id, Supervisor supervisorModel);
        Task<Supervisor?> DeleteAsync(int id);
        Task<bool> SupervisorExists(int id);
    }
}