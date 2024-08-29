using Enwage_API.DTOs;

namespace Enwage_API.Services
{
    public interface IEmployeeService
    {
        Task<PaginatedResult<EmployeeDto>> GetPaginatedEmployeesAsync(int pageNumber, int pageSize, string searchQuery);
        Task<EmployeeDto> GetEmployeeByIdAsync(Guid id);
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto employeeDto);
        Task<EmployeeDto> UpdateEmployeeAsync(Guid id, EmployeeDto employeeDto);
        Task DeleteEmployeeAsync(Guid id);
        Task AddStateToEmployeeAsync(Guid employeeId, Guid stateId);
        Task RemoveStateFromEmployeeAsync(Guid employeeId, Guid stateId);
    }
}