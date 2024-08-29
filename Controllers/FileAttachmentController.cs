using Enwage_API.DTOs;
using Enwage_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Enwage_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileAttachmentsController : ControllerBase
    {
        private readonly IFileAttachmentService _fileAttachmentService;

        public FileAttachmentsController(IFileAttachmentService fileAttachmentService)
        {
            _fileAttachmentService = fileAttachmentService;
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<FileAttachmentDto>>> GetFilesByEmployeeId(Guid employeeId)
        {
            var files = await _fileAttachmentService.GetFilesByEmployeeIdAsync(employeeId);
            return Ok(files);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FileAttachmentDto>> GetFile(Guid id)
        {
            var file = await _fileAttachmentService.GetFileByIdAsync(id);
            if (file == null)
            {
                return NotFound();
            }
            return Ok(file);
        }

        [HttpPost("employee/{employeeId}")]
        public async Task<ActionResult<FileAttachmentDto>> AddFile(Guid employeeId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is required");
            }

            var addedFile = await _fileAttachmentService.AddFileAsync(employeeId, file);
            return CreatedAtAction(nameof(GetFile), new { id = addedFile.Id }, addedFile);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            try
            {
                await _fileAttachmentService.DeleteFileAsync(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadFile(Guid id)
        {
            try
            {
                var (fileContents, contentType, fileName) = await _fileAttachmentService.DownloadFileAsync(id);
                return File(fileContents, contentType, fileName);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}
