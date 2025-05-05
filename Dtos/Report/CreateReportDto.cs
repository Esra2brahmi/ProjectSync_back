namespace projectSync_back.Dtos.Report
{
    public class CreateReportDto
    {
        
        public DateTime Deadline { get; set; }
        public int ProjectId { get; set; }
        public int JuryMemberId { get; set; }
        
        // File will be uploaded separately, not in this DTO
    }
}