using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectSync_back.Dtos.Project
{
    public class UpdateProjectRequestDto
    {
        public string ProjectName { get; set; } =string.Empty;
        public string SupervisorFirstName { get; set; } =string.Empty;
        public string SupervisorLastName { get; set; } =string.Empty;
        public string Status { get; set; } =string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}