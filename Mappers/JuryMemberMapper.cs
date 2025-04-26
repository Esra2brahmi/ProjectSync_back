using projectSync_back.Dtos.JuryMember;
using projectSync_back.Models;

namespace projectSync_back.Mappers
{
    public static class JuryMemberMapper
    {
        public static JuryMemberDto ToJuryMemberDto(this JuryMember juryMemberModel)
        {
            return new JuryMemberDto
            {
                Id = juryMemberModel.Id,
                FirstName = juryMemberModel.FirstName,
                LastName = juryMemberModel.LastName,
                Email = juryMemberModel.Email,
                PhoneNumber = juryMemberModel.PhoneNumber,
                Description = juryMemberModel.Description,
                Address = juryMemberModel.Address,
                AcademicTitle = juryMemberModel.AcademicTitle,
                 DepartmentIds = juryMemberModel.DepJuryMembers?
                    .Select(ds => ds.DepartmentId)
                    .ToList() ?? new List<int>()
            };
        }

          public static JuryMember ToJuryMemberFromCreateDto(this CreateJuryMemberDto juryMemberDto)
            {
                return new JuryMember
                {
                    FirstName = juryMemberDto.FirstName,
                    LastName = juryMemberDto.LastName,
                    Email = juryMemberDto.Email,
                    PhoneNumber = juryMemberDto.PhoneNumber,
                    Description = juryMemberDto.Description,
                    Address = juryMemberDto.Address,
                    AcademicTitle = juryMemberDto.AcademicTitle,
                    // Initialize the collections
                    DepJuryMembers = juryMemberDto.DepartmentIds?
                        .Select(id => new DepJuryMember { DepartmentId = id })
                        .ToList() ?? new List<DepJuryMember>()
                };
            }
    }
}