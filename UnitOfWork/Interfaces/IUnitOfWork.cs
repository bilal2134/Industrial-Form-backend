using Enwage_API.Models;
using Enwage_API.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Enwage_API.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IFileAttachmentRepository FileAttachments { get; }
        IStatenameRepository Statenames { get; }
        IClientRepository Clients { get; }
        IEmployeeStatenameRepository EmployeeStatenameRepository { get; }
        Task CompleteAsync();
        Enwage2Context GetContext();
    }
}