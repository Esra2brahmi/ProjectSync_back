using projectSync_back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace projectSync_back.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetAllAsync();
        Task<Report> GetByIdAsync(int id);
        Task<Report> GetByProjectIdAsync(int projectId);
        Task<IEnumerable<Report>> GetByJuryMemberIdAsync(int juryMemberId);
        Task<Report> CreateAsync(Report report);
        Task<Report> UpdateAsync(Report report);
        Task<bool> DeleteAsync(int id);
        Task<bool> Exists(int id);
        Task<bool> ProjectHasReport(int projectId);
        Task<IEnumerable<Report>> GetReportsByProjectLevelAsync(string level);
    }
}