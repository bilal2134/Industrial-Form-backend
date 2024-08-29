using Enwage_API.Models;
using Enwage_API.Repositories.Interfaces;
using Enwage_API.Repositories;
using Enwage_API.UnitOfWork.Interfaces;

namespace Enwage_API.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Enwage2Context _context;
        private IEmployeeRepository _employeeRepository;
        private IFileAttachmentRepository _fileAttachmentRepository;
        private IStatenameRepository _statenameRepository;
        private IClientRepository _clientRepository;
        private IEmployeeStatenameRepository _employeeStatenameRepository;

        public UnitOfWork(Enwage2Context context)
        {
            _context = context;
        }

        public IEmployeeRepository Employees => _employeeRepository ??= new EmployeeRepository(_context);
        public IFileAttachmentRepository FileAttachments => _fileAttachmentRepository ??= new FileAttachmentRepository(_context);
        public IStatenameRepository Statenames => _statenameRepository ??= new StatenameRepository(_context);
        public IClientRepository Clients => _clientRepository ??= new ClientRepository(_context);
        public IEmployeeStatenameRepository EmployeeStatenameRepository => _employeeStatenameRepository ??= new EmployeeStatenameRepository(_context);

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Enwage2Context GetContext()
        {
            return _context;
        }
    }
}