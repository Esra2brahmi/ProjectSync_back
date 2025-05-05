using Microsoft.EntityFrameworkCore;
using projectSync_back.data;
using projectSync_back.Interfaces;
using projectSync_back.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectSync_back.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDBContext _context;

        public ReportRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> GetAllAsync()
        {
            return await _context.Reports
                .Include(r => r.Project)
                .Include(r => r.JuryMember)
                .ToListAsync();
        }

        public async Task<Report> GetByIdAsync(int id)
        {
            return await _context.Reports
                .Include(r => r.Project)
                .Include(r => r.JuryMember)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Report> GetByProjectIdAsync(int projectId)
        {
            return await _context.Reports
                .Include(r => r.JuryMember)
                .FirstOrDefaultAsync(r => r.ProjectId == projectId);
        }

        public async Task<IEnumerable<Report>> GetByJuryMemberIdAsync(int juryMemberId)
        {
            return await _context.Reports
                .Include(r => r.Project)
                .Where(r => r.JuryMemberId == juryMemberId)
                .ToListAsync();
        }

        public async Task<Report> CreateAsync(Report report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report> UpdateAsync(Report report)
        {
            var existingReport = await _context.Reports.FindAsync(report.Id);
            if (existingReport == null)
            {
                return null;
            }

            // Update properties
            existingReport.Deadline = report.Deadline;
            existingReport.JuryMemberId = report.JuryMemberId;
            // Don't update ProjectId as it's a one-to-one relationship
            // File properties would typically be updated through a separate method

            await _context.SaveChangesAsync();
            return existingReport;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return false;
            }

            _context.Reports.Remove(report);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Reports.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> ProjectHasReport(int projectId)
        {
            return await _context.Reports.AnyAsync(r => r.ProjectId == projectId);
        }
        public async Task<IEnumerable<Report>> GetReportsByProjectLevelAsync(string level)
        {
            return await _context.Reports
                .Include(r => r.Project)
                .Include(r => r.JuryMember)
                .Where(r => r.Project.Level == level)
                .ToListAsync();
        }
    }
}