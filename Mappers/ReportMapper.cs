using projectSync_back.Dtos.Report;
using projectSync_back.Models;

namespace projectSync_back.Mappers
{
    public static class ReportMapper
    {
        public static ReportDto ToReportDto(this Report report)
        {
            return new ReportDto
            {
                Id = report.Id,
                Deadline = report.Deadline,
                ProjectId = report.ProjectId,
                ProjectName = report.Project?.ProjectName ?? string.Empty,
                JuryMemberId = report.JuryMemberId,
                JuryMemberName = report.JuryMember != null 
                    ? $"{report.JuryMember.FirstName} {report.JuryMember.LastName}" 
                    : string.Empty,
                FileName = report.FileName,
                FileSize = report.FileSize,
                FileFormat = report.FileFormat,
                UploadDate = report.UploadDate
            };
        }

        public static Report ToReportFromCreateDto(this CreateReportDto dto)
        {
            return new Report
            {
                Deadline = dto.Deadline,
                ProjectId = dto.ProjectId,
                JuryMemberId = dto.JuryMemberId,
                UploadDate = DateTime.UtcNow
            };
        }
    }
}