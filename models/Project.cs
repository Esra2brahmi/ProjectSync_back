using System;
using System.ComponentModel.DataAnnotations; 

namespace projectSync_back.Models
{
    public class Project
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(100, ErrorMessage = "Project name can't be longer than 100 characters.")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [Range(typeof(DateTime), "1/1/2022", "12/31/2099", ErrorMessage = "End date must be a valid date.")]
        public DateTime EndDate { get; set;}
        public required string SupervisorFirstName { get; set; }
        public required string SupervisorLastName { get; set; }
        public string Department { get; set; }
        public string Level { get; set; }

        public List<ProjectTask> ProjectTasks {get;set;} = new List<ProjectTask>();
        public List<ProjectSupervisor> ProjectSupervisors {get;set;} = new List<ProjectSupervisor>();
        public List<User> Users { get; set; } = new List<User>();



        
        public string SupervisorFullName => $"{SupervisorFirstName} {SupervisorLastName}";

        
        public bool IsValid()
        {
            return StartDate < EndDate;
        }
    }
}
