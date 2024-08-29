using Enwage_API.DTOs;

namespace Enwage_API.Services
{
    public interface IFileAttachmentService
    {
        Task<IEnumerable<FileAttachmentDto>> GetFilesByEmployeeIdAsync(Guid employeeId);
        Task<FileAttachmentDto> GetFileByIdAsync(Guid id);
        Task<FileAttachmentDto> AddFileAsync(Guid employeeId, IFormFile file);
        Task DeleteFileAsync(Guid id);
        Task<(byte[] FileContents, string ContentType, string FileName)> DownloadFileAsync(Guid id);
    }
}