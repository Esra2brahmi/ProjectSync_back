using System;
using System.ComponentModel.DataAnnotations; 

namespace projectSync_back.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name {get;set;} =string.Empty;
        public string ChairName {get;set;} =string.Empty;
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } =string.Empty;
        public List<DepSupervisor> DepSupervisors {get;set;} = new List<DepSupervisor>();


    }
}
