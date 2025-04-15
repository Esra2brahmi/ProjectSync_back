using System;
using System.ComponentModel.DataAnnotations;

namespace projectSync_back.Models
{
    public class Supervisor 
    {
        public int Id { get; set; } 
        public string FirstName { get; set; } =string.Empty;
        public string LastName { get; set; }=string.Empty;
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } =string.Empty;
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; } = string.Empty; 
        public string Description {get;set;} =string.Empty;
        public string Address {get;set;} =string.Empty;
        public string AcademicTitle {get;set;} =string.Empty;
        public List<ProjectSupervisor> ProjectSupervisors {get;set;} = new List<ProjectSupervisor>();
        public List<DepSupervisor> DepSupervisors {get;set;} = new List<DepSupervisor>();

      
    }
}
