using System;
using System.ComponentModel.DataAnnotations;

namespace projectSync_back.Models
{
    public class User 
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "User firstName is required.")]
        [StringLength(100, ErrorMessage = "User firstName can't be longer than 100 characters.")]
        public string UserFirstName { get; set; } =string.Empty;

        [Required(ErrorMessage = "User lastName is required.")]
        [StringLength(100, ErrorMessage = "User lastName can't be longer than 100 characters.")]
        public string UserLastName { get; set; } =string.Empty;

        [Required(ErrorMessage = "Department is required.")]
        [StringLength(100, ErrorMessage = "Department can't be longer than 100 characters.")]
        public string Department { get; set; } =string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } =string.Empty;

        [Required(ErrorMessage = "Class is required.")]
        [StringLength(100, ErrorMessage = "Class can't be longer than 100 characters.")]
        public string Classe { get; set; } =string.Empty;

        [Required(ErrorMessage = "ProjectType is required.")]
        [StringLength(100, ErrorMessage = "ProjectType can't be longer than 100 characters:{PFA1 or PFA2}.")]
        public string ProjectType { get; set; } =string.Empty;

        [Required(ErrorMessage = "SupervisorFullName is required.")]
        [StringLength(100, ErrorMessage = "SupervisorFullName can't be longer than 100 characters.")]
        public string SupervisorFullName { get; set; } =string.Empty;









    }
}
