using Microsoft.AspNetCore.Mvc;
using projectSync_back.Dtos.Report;
using projectSync_back.Interfaces;
using projectSync_back.Mappers;
using projectSync_back.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace projectSync_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IJuryMemberRepository _juryMemberRepository;
        private readonly string _uploadsFolder;

        public ReportsController(
            IReportRepository reportRepository,
            IProjectRepository projectRepository,
            IJuryMemberRepository juryMemberRepository)
        {
            _reportRepository = reportRepository;
            _projectRepository = projectRepository;
            _juryMemberRepository = juryMemberRepository;
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "reports");
            
            // Ensure the directory exists
            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }
        }

        // GET: api/Reports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetAll()
        {
            var reports = await _reportRepository.GetAllAsync();
            var reportDtos = reports.Select(r => r.ToReportDto());
            return Ok(reportDtos);
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportDto>> GetById(int id)
        {
            var report = await _reportRepository.GetByIdAsync(id);
            if (report == null) return NotFound();
            
            return Ok(report.ToReportDto());
        }

        // GET: api/Reports/project/5
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<ReportDto>> GetByProjectId(int projectId)
        {
            var report = await _reportRepository.GetByProjectIdAsync(projectId);
            if (report == null) return NotFound();
            
            return Ok(report.ToReportDto());
        }

        // GET: api/Reports/jurymember/5
        [HttpGet("jurymember/{juryMemberId}")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetByJuryMemberId(int juryMemberId)
        {
            var reports = await _reportRepository.GetByJuryMemberIdAsync(juryMemberId);
            var reportDtos = reports.Select(r => r.ToReportDto());
            return Ok(reportDtos);
        }

        // POST: api/Reports
        [HttpPost]
        public async Task<ActionResult<ReportDto>> Create([FromBody] CreateReportDto createDto)
        {
            // Check if project exists
            var project = await _projectRepository.GetByIdAsync(createDto.ProjectId);
            if (project == null) return BadRequest("Project not found");
            
            // Check if jury member exists
            var juryMember = await _juryMemberRepository.GetByIdAsync(createDto.JuryMemberId);
            if (juryMember == null) return BadRequest("Jury member not found");
            
            // Check if project already has a report (one-to-one relationship)
            if (await _reportRepository.ProjectHasReport(createDto.ProjectId))
                return BadRequest("Project already has a report");
            
            var report = createDto.ToReportFromCreateDto();
            var createdReport = await _reportRepository.CreateAsync(report);
            
            return CreatedAtAction(nameof(GetById), new { id = createdReport.Id }, createdReport.ToReportDto());
        }

        // PUT: api/Reports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReportDto updateDto)
        {
            if (id != updateDto.Id) return BadRequest("ID mismatch");
            
            var report = await _reportRepository.GetByIdAsync(id);
            if (report == null) return NotFound();
            
            // Check if jury member exists
            var juryMember = await _juryMemberRepository.GetByIdAsync(updateDto.JuryMemberId);
            if (juryMember == null) return BadRequest("Jury member not found");
            
            // Update properties
            report.Deadline = updateDto.Deadline;
            report.JuryMemberId = updateDto.JuryMemberId;
            
            var updatedReport = await _reportRepository.UpdateAsync(report);
            if (updatedReport == null) return NotFound();
            
            return NoContent();
        }

        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var report = await _reportRepository.GetByIdAsync(id);
            if (report == null) return NotFound();
            
            // Delete the file if it exists
            if (!string.IsNullOrEmpty(report.FilePath) && System.IO.File.Exists(report.FilePath))
            {
                System.IO.File.Delete(report.FilePath);
            }
            
            var success = await _reportRepository.DeleteAsync(id);
            if (!success) return NotFound();
            
            return NoContent();
        }

        // POST: api/Reports/5/upload
        [HttpPost("{id}/upload")]
        public async Task<IActionResult> UploadFile(int id, IFormFile file)
        {
            var report = await _reportRepository.GetByIdAsync(id);
            if (report == null) return NotFound();
            
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");
            
            // Check if the file is a PDF
            if (!file.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only PDF files are allowed");
            
            // Generate a unique filename
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(_uploadsFolder, fileName);
            
            // Delete the old file if it exists
            if (!string.IsNullOrEmpty(report.FilePath) && System.IO.File.Exists(report.FilePath))
            {
                System.IO.File.Delete(report.FilePath);
            }
            
            // Save the new file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            // Update report properties
            report.FileName = file.FileName;
            report.FilePath = filePath;
            report.FileSize = file.Length;
            report.FileFormat = "pdf";
            report.UploadDate = DateTime.UtcNow;
            
            await _reportRepository.UpdateAsync(report);
            
            return Ok(report.ToReportDto());
        }

        // GET: api/Reports/5/download
        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var report = await _reportRepository.GetByIdAsync(id);
            if (report == null) return NotFound();
            
            if (string.IsNullOrEmpty(report.FilePath) || !System.IO.File.Exists(report.FilePath))
                return NotFound("File not found");
            
            var fileBytes = await System.IO.File.ReadAllBytesAsync(report.FilePath);
            return File(fileBytes, "application/pdf", report.FileName);
        }
        // GET: api/Reports/level/{level}
        [HttpGet("level/{level}")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetByProjectLevel(string level)
        {
            var reports = await _reportRepository.GetReportsByProjectLevelAsync(level);
            
            if (reports == null || !reports.Any())
                return NotFound($"No reports found for projects with level '{level}'");
            
            var reportDtos = reports.Select(r => r.ToReportDto());
            return Ok(reportDtos);
        }
    }
}