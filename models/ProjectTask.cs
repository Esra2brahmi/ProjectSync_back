using System;
using System.ComponentModel.DataAnnotations;

namespace projectSync_back.Models
{
    public class ProjectTask 
    {
        public int Id { get; set; } // Primary key

        public int? ProjectId {get;set;}
        public Project? Project {get;set;}

        [Required(ErrorMessage = "Task name is required.")]
        [StringLength(100, ErrorMessage = "Task name can't be longer than 100 characters.")]
        public string TaskName { get; set; } =string.Empty;

        [Required(ErrorMessage = "Task description is required.")]
        [StringLength(500, ErrorMessage = "Task description can't be longer than 500 characters.")]
        public string TaskDescription { get; set; }=string.Empty;

        [Required(ErrorMessage = "Due date is required.")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
