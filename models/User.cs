using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectSync_back.Models
{
    public class User 
    {
        public int Id { get; set; } 

        
        public string UserFirstName { get; set; } =string.Empty;
        public string UserLastName { get; set; } =string.Empty;
        public string Department { get; set; } =string.Empty;
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } =string.Empty;
        public string Classe { get; set; } =string.Empty;
        public string ProjectType { get; set; } =string.Empty;
        public string SupervisorFullName { get; set; } =string.Empty;
        public int? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }









    }
}
