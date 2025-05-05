using System;
using System.ComponentModel.DataAnnotations;

namespace projectSync_back.Models
{
    public class JuryMember 
    {
        public int Id { get; set; } // Primary key

        [Required(ErrorMessage = "juryMember firstName is required.")]
        [StringLength(100, ErrorMessage = "juryMember firstName can't be longer than 100 characters.")]
        public string FirstName { get; set; } =string.Empty;

        [Required(ErrorMessage = "juryMemberlastName is required.")]
        [StringLength(100, ErrorMessage = "juryMember lastName can't be longer than 100 characters.")]
        public string LastName { get; set; } =string.Empty;

        [Required(ErrorMessage = "PhoneNumber is required.")]
        [StringLength(100, ErrorMessage = "PhoneNumber can't be longer than 100 characters:{PFA1 or PFA2}.")]
        public string PhoneNumber { get; set; }  =string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } =string.Empty;

        [Required(ErrorMessage = "Adress is required.")]
        [StringLength(100, ErrorMessage = "Adress can't be longer than 100 characters.")]
        public string Address { get; set; } =string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100, ErrorMessage = "Description can't be longer than 100 characters:{PFA1 or PFA2}.")]
        public string Description { get; set; } =string.Empty;

        [Required(ErrorMessage = "AcademicTitle is required.")]
        [StringLength(100, ErrorMessage = "AcademicTitle can't be longer than 100 characters.")]
        public string AcademicTitle { get; set; } =string.Empty;

        public List<DepJuryMember> DepJuryMembers {get;set;} = new List<DepJuryMember >();
        // report
        public List<Report> Reports { get; set; } = new List<Report>();









    }
}
