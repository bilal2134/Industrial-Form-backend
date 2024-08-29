using AutoMapper;
using Enwage_API.DTOs;
using Enwage_API.Models;
using Enwage_API.UnitOfWork.Interfaces;

namespace Enwage_API.Services
{
    public class FileAttachmentService : IFileAttachmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FileAttachmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FileAttachmentDto>> GetFilesByEmployeeIdAsync(Guid employeeId)
        {
            var files = await _unitOfWork.FileAttachments.GetFilesByEmployeeIdAsync(employeeId);
            return _mapper.Map<IEnumerable<FileAttachmentDto>>(files);
        }

        public async Task<FileAttachmentDto> GetFileByIdAsync(Guid id)
        {
            var file = await _unitOfWork.FileAttachments.GetByIdAsync(id);
            return _mapper.Map<FileAttachmentDto>(file);
        }

        public async Task<FileAttachmentDto> AddFileAsync(Guid employeeId, IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var fileAttachment = new Fileattachment
            {
                Id = Guid.NewGuid(), // Generate a new unique Id
                Employeeid = employeeId,
                Filename = file.FileName,
                Contenttype = file.ContentType,
                Filedata = memoryStream.ToArray(),
                Uploaddate = DateTime.UtcNow
            };

            await _unitOfWork.FileAttachments.AddAsync(fileAttachment);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<FileAttachmentDto>(fileAttachment);
        }

        public async Task DeleteFileAsync(Guid id)
        {
            var file = await _unitOfWork.FileAttachments.GetByIdAsync(id);
            if (file == null)
            {
                throw new ArgumentException("File not found");
            }

            _unitOfWork.FileAttachments.Remove(file);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<(byte[] FileContents, string ContentType, string FileName)> DownloadFileAsync(Guid id)
        {
            var file = await _unitOfWork.FileAttachments.GetFileWithDataAsync(id);
            if (file == null)
            {
                throw new ArgumentException("File not found");
            }

            return (file.Filedata, file.Contenttype, file.Filename);
        }
    }
}