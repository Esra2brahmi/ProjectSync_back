using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectSync_back.Dtos.Department
{


    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ChairName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}