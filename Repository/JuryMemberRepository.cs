using Microsoft.EntityFrameworkCore;
using projectSync_back.data;
using projectSync_back.Interfaces;
using projectSync_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectSync_back.Dtos.JuryMember;



namespace projectSync_back.Repository
{
    public class JuryMemberRepository : IJuryMemberRepository
    {
        private readonly ApplicationDBContext _context;

        public JuryMemberRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<JuryMember>> GetAllAsync()
        {
            return await _context.JuryMembers
            .Include(s => s.DepJuryMembers)  // Include the join table
            .ToListAsync();
        }

        public async Task<JuryMember?> GetByIdAsync(int id)
        {
            return await _context.JuryMembers.FindAsync(id);
        }

        public async Task<JuryMember> CreateAsync(JuryMember juryMemberModel, List<int> departmentIds)
{
    using var transaction = await _context.Database.BeginTransactionAsync();
    
    try
    {
        // 1. Add JuryMember first
        await _context.JuryMembers.AddAsync(juryMemberModel);
        await _context.SaveChangesAsync();

        // 2. Process department associations
        if (departmentIds != null && departmentIds.Any())
        {
            // Get existing associations to avoid duplicates
            var existingDeptIds = await _context.DepJuryMembers
                .Where(ds => ds.JuryMemberId == juryMemberModel.Id)
                .Select(ds => ds.DepartmentId)
                .ToListAsync();

            // Only add new associations that don't already exist
            var newDeptIds = departmentIds.Except(existingDeptIds);
            
            foreach (var departmentId in newDeptIds)
            {
                _context.DepJuryMembers.Add(new DepJuryMember
                {
                    JuryMemberId = juryMemberModel.Id,
                    DepartmentId = departmentId
                });
            }
            
            if (newDeptIds.Any())
            {
                await _context.SaveChangesAsync();
            }
        }

        await transaction.CommitAsync();
        return juryMemberModel;
    }
    catch
    {
        await transaction.RollbackAsync();
        throw;
    }
}

        public async Task<JuryMember?> UpdateAsync(int id, JuryMember juryMemberModel)
        {
            var existingJuryMember= await _context.JuryMembers.FindAsync(id);
            if (existingJuryMember == null)
            {
                return null;
            }

            existingJuryMember.FirstName = juryMemberModel.FirstName;
            existingJuryMember.LastName = juryMemberModel.LastName;
            existingJuryMember.Email = juryMemberModel.Email;
            existingJuryMember.PhoneNumber = juryMemberModel.PhoneNumber;
            existingJuryMember.Description = juryMemberModel.Description;
            existingJuryMember.Address = juryMemberModel.Address;
            existingJuryMember.AcademicTitle= juryMemberModel.AcademicTitle;

            await _context.SaveChangesAsync();
            return existingJuryMember;
        }

        public async Task<JuryMember?> DeleteAsync(int id)
        {
            var juryMemberModel = await _context.JuryMembers.FindAsync(id);
            if (juryMemberModel == null)
            {
                return null;
            }

            _context.JuryMembers.Remove(juryMemberModel);
            await _context.SaveChangesAsync();
            return juryMemberModel;
        }

        public async Task<bool> JuryMemberExists(int id)
        {
            return await _context.JuryMembers.AnyAsync(s => s.Id == id);
        }
    }
}