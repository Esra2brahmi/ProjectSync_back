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
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Supervisor first name is required.")]
        [StringLength(100, ErrorMessage = "Supervisor first name can't be longer than 100 characters.")]
        public required string SupervisorFirstName { get; set; }

        [Required(ErrorMessage = "Supervisor last name is required.")]
        [StringLength(100, ErrorMessage = "Supervisor last name can't be longer than 100 characters.")]
        public required string SupervisorLastName { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Level is required.")]
        public string Level { get; set; }

        public List<ProjectTask> ProjectTasks {get;set;} = new List<ProjectTask>();
        public List<ProjectSupervisor> ProjectSupervisors {get;set;} = new List<ProjectSupervisor>();



        
        public string SupervisorFullName => $"{SupervisorFirstName} {SupervisorLastName}";

        
        public bool IsValid()
        {
            return StartDate < EndDate;
        }
    }
}
