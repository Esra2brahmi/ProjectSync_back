using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace projectSync_back.Dtos.JuryMember
{
    public class JuryMemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AcademicTitle { get; set; } = string.Empty;
        public List<int> DepartmentIds { get; set; } = new List<int>();
    }
}