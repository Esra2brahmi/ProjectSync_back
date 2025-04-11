using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectSync_back.data;
using projectSync_back.Dtos.Attachment;
using projectSync_back.Models;
using projectSync_back.Mappers;
using Microsoft.EntityFrameworkCore;
using projectSync_back.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace projectSync_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<AttachmentController> _logger;

        public AttachmentController(
            IAttachmentRepository attachmentRepository, 
            IWebHostEnvironment environment,ILogger<AttachmentController> logger)
        {
            _attachmentRepository = attachmentRepository;
            _environment = environment;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<AttachmentDto>> Upload([FromForm] CreateAttachmentDto attachmentDto)
        {
            try
            {
                // Validate input
                if (attachmentDto == null)
                {
                    _logger.LogWarning("Upload attempt with null DTO");
                    return BadRequest("Upload data is required");
                }

                if (attachmentDto.File == null || attachmentDto.File.Length == 0)
                {
                    _logger.LogWarning("Upload attempt with empty file");
                    return BadRequest("File is required");
                }

                _logger.LogInformation($"Starting upload for file: {attachmentDto.File.FileName}");

                var attachment = await _attachmentRepository.UploadAttachmentAsync(attachmentDto);
                var resultDto = attachment.ToDto(Url);
                
                _logger.LogInformation($"Successfully uploaded file: {attachmentDto.File.FileName}");
                return CreatedAtAction(nameof(Get), new { id = attachment.Id }, resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading attachment");
                return StatusCode(500, "An error occurred while processing your upload");
            }
        }

        [HttpGet("task/{taskId}")]
        public async Task<ActionResult<IEnumerable<AttachmentDto>>> GetByTaskId(int taskId)
        {
            var attachments = await _attachmentRepository.GetAttachmentsByTaskIdAsync(taskId);
            return Ok(attachments.Select(a => a.ToDto(Url)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttachmentDto>> Get(int id)
        {
            var attachment = await _attachmentRepository.GetAttachmentByIdAsync(id);
            if (attachment == null) return NotFound();
            return Ok(attachment.ToDto(Url));
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var attachment = await _attachmentRepository.GetAttachmentByIdAsync(id);
            if (attachment == null) return NotFound();

            var filePath = await _attachmentRepository.GetFilePathAsync(id);
            if (!System.IO.File.Exists(filePath)) return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, attachment.ContentType, attachment.FileName);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _attachmentRepository.DeleteAttachmentAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}