using System;
using System.ComponentModel.DataAnnotations;

namespace projectSync_back.Models
{
    public class DepJuryMember 
    {
        public int DepartmentId { get; set; }
        public int JuryMemberId { get; set; }  
        public Department Department {get;set;}
        public JuryMember JuryMember {get;set;}

        
    }
}
