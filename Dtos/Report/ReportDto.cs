namespace projectSync_back.Dtos.Report
{
    public class ReportDto
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int JuryMemberId { get; set; }
        public string JuryMemberName { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FileFormat { get; set; }
        public DateTime UploadDate { get; set; }
    }
}