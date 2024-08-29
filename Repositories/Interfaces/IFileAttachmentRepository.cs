using Enwage_API.Models;

namespace Enwage_API.Repositories.Interfaces
{
    public interface IFileAttachmentRepository : IGenericRepository<Fileattachment>
    {
        Task<IEnumerable<Fileattachment>> GetFilesByEmployeeIdAsync(Guid employeeId);
        Task<Fileattachment> GetFileWithDataAsync(Guid id);
    }
}