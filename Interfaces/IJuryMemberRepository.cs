using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectSync_back.Dtos.JuryMember;
using projectSync_back.Models;



namespace projectSync_back.Interfaces
{
    public interface IJuryMemberRepository
    {
        Task<List<JuryMember>> GetAllAsync();
        Task<JuryMember?> GetByIdAsync(int id);
        Task<JuryMember> CreateAsync(JuryMember juryMemberModel, List<int> departmentIds);
        Task<JuryMember?> UpdateAsync(int id, JuryMember juryMemberModel);
        Task<JuryMember?> DeleteAsync(int id);
        Task<bool> JuryMemberExists(int id);
    }
}