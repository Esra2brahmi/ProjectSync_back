using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectSync_back.Models
{
    public class ProjectTask 
    {
        public int Id { get; set; } 

        public int? ProjectId {get;set;}
        public Project? Project {get;set;}
        [ForeignKey("User")]
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("ProjectTasks")]  // Explicitly maps to User.ProjectTasks
        public User? User { get; set; }  
        public string TaskName { get; set; } =string.Empty;
        public string TaskDescription { get; set; }=string.Empty;
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

    }
}
