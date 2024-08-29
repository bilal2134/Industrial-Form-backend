using Enwage_API.DTOs;
using Enwage_API.Models;

namespace Enwage_API.Repositories.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<PaginatedResult<EmployeeDto>> GetPaginatedAsync(int pageNumber, int pageSize, string searchQuery);
        Task<Employee> GetEmployeeWithStatesAsync(Guid id);
        Task AddStateToEmployeeAsync(Guid employeeId, Guid stateId);
        Task RemoveStateFromEmployeeAsync(Guid employeeId, Guid stateId);

        Task<Employee> GetEmployeeWithClientAsync(Guid id);




        // new method to check email
        
         Task<bool> EmailExistsAsync(string email);
        

    }
}