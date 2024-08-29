
using Enwage_API.Models;
using Enwage_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Enwage_API.Repositories
{
    public class FileAttachmentRepository : GenericRepository<Fileattachment>, IFileAttachmentRepository
    {
        public FileAttachmentRepository(Enwage2Context context) : base(context) { }

        public async Task<IEnumerable<Fileattachment>> GetFilesByEmployeeIdAsync(Guid employeeId)
        {
            return await _context.Fileattachments
                .Where(f => f.Employeeid == employeeId)
                .ToListAsync();
        }

        public async Task<Fileattachment> GetFileWithDataAsync(Guid id)
        {
            return await _context.Fileattachments
                .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}