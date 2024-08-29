using System;

namespace Enwage_API.DTOs
{
    public class FileAttachmentDto
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public DateTime? UploadDate { get; set; }
    }
}