namespace projectSync_back.Dtos.Report
{
    public class UpdateReportDto
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public int JuryMemberId { get; set; }
        // ProjectId typically wouldn't be updated once set
    }
}