using System;
using System.ComponentModel.DataAnnotations;

namespace projectSync_back.Models
{
    public class ProjectSupervisor 
    {
        public int ProjectId { get; set; }
        public int SupervisorId { get; set; }  
        public Project Project {get;set;}
        public Supervisor Supervisor {get;set;}

        
    }
}
