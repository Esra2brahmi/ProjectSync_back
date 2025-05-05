using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectSync_back.Models
{
    public class Report
    {
        public int Id { get; set; }
        
        [Required]
        public DateTime Deadline { get; set; }
        
        // One-to-one relationship with Project
        public int ProjectId { get; set; }
        
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        
        // Many-to-one relationship with JuryMember
        public int JuryMemberId { get; set; }
        
        [ForeignKey("JuryMemberId")]
        public JuryMember JuryMember { get; set; }
        
        // PDF document properties
        [Required]
        public string FileName { get; set; } = string.Empty;
        
        public string FilePath { get; set; } = string.Empty;
        
        public long FileSize { get; set; }
        
        public string FileFormat { get; set; } = string.Empty;
        
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    }
}