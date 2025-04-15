using System;
using System.ComponentModel.DataAnnotations;

namespace projectSync_back.Models
{
    public class DepSupervisor 
    {
        public int DepartmentId { get; set; }
        public int SupervisorId { get; set; }  
        public Department Department {get;set;}
        public Supervisor Supervisor {get;set;}

        
    }
}
