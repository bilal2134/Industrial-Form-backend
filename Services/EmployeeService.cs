using AutoMapper;
using Enwage_API.DTOs;
using Enwage_API.Models;
using Enwage_API.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Enwage_API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<EmployeeDto>> GetPaginatedEmployeesAsync(int pageNumber, int pageSize, string searchQuery)
        {
            if (_unitOfWork == null || _unitOfWork.Employees == null)
            {
                throw new InvalidOperationException("UnitOfWork or Employees repository is not initialized.");
            }

            var result = await _unitOfWork.Employees.GetPaginatedAsync(pageNumber, pageSize, searchQuery);
            var employeeDtos = _mapper.Map<List<EmployeeDto>>(result.Items);

            return new PaginatedResult<EmployeeDto>
            {
                TotalCount = result.TotalCount,
                PageSize = result.PageSize,
                CurrentPage = result.CurrentPage,
                TotalPages = result.TotalPages,
                Items = employeeDtos
            };
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _unitOfWork.Employees.GetEmployeeWithStatesAsync(id);
            if (employee == null)
            {
                throw new ArgumentException("Employee not found");
            }
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto employeeDto)
        {

            // Check if the email already exists
            if (await _unitOfWork.Employees.EmailExistsAsync(employeeDto.Email))
            {
                throw new ArgumentException("An employee with this email already exists.");
            }


            // Generate a new unique Id for the new employee
            var newEmployee = _mapper.Map<Employee>(employeeDto);
            newEmployee.Id = Guid.NewGuid();
            await _unitOfWork.Employees.AddAsync(newEmployee);
            foreach (var stateId in employeeDto.States)
            {
                await _unitOfWork.EmployeeStatenameRepository.AddEmployeeState(new EmployeeStatename
                {
                    EmployeeId = newEmployee.Id,
                    StatenameId = stateId
                });
            }

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<EmployeeDto>(newEmployee);
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(Guid id, EmployeeDto employeeDto)
        {
            var existingEmployee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                throw new ArgumentException("Employee not found");
            }

            // Update employee details
            existingEmployee.Name = employeeDto.Name;
            existingEmployee.Email = employeeDto.Email;
            existingEmployee.ClientId = employeeDto.ClientId;
            existingEmployee.Dateofbirth = employeeDto.DateOfBirth;
            existingEmployee.Experiencestartdate = employeeDto.ExperienceStartDate;
            existingEmployee.Experienceenddate = employeeDto.ExperienceEndDate;
            existingEmployee.Hourlyrate = employeeDto.HourlyRate;
            existingEmployee.Changestate = employeeDto.ChangeState;
            existingEmployee.Gender = employeeDto.Gender;
            existingEmployee.Ispresent = employeeDto.IsPresent;

            // Clear existing states
            var existingStates = await _unitOfWork.EmployeeStatenameRepository.FindAsync(es => es.EmployeeId == id);
            foreach (var state in existingStates)
            {
                _unitOfWork.EmployeeStatenameRepository.Remove(state);
            }

            // Add new states
            foreach (var stateId in employeeDto.States)
            {
                existingEmployee.EmployeeStatenames.Add(new EmployeeStatename
                {
                    EmployeeId = id,
                    StatenameId = stateId
                });
            }

            _unitOfWork.Employees.Update(existingEmployee);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<EmployeeDto>(existingEmployee);
        }






        public async Task DeleteEmployeeAsync(Guid id)
        {
            var existingEmployee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                throw new ArgumentException("Employee not found");
            }

            // Delete related EmployeeStatename entities
            var employeeStatenames = await _unitOfWork.EmployeeStatenameRepository.FindAsync(es => es.EmployeeId == id);
            foreach (var employeeStatename in employeeStatenames)
            {
                _unitOfWork.EmployeeStatenameRepository.Remove(employeeStatename);
            }

            // Delete related Fileattachment entities
            var fileAttachments = await _unitOfWork.FileAttachments.FindAsync(f => f.Employeeid == id);
            foreach (var fileAttachment in fileAttachments)
            {
                _unitOfWork.FileAttachments.Remove(fileAttachment);
            }

            // Delete the employee
            _unitOfWork.Employees.Remove(existingEmployee);
            await _unitOfWork.CompleteAsync();
        }

        public async Task AddStateToEmployeeAsync(Guid employeeId, Guid stateId)
        {
            var employee = await _unitOfWork.Employees.GetEmployeeWithStatesAsync(employeeId);
            if (employee == null)
            {
                throw new ArgumentException("Employee not found");
            }

            var state = await _unitOfWork.Statenames.GetByIdAsync(stateId);
            if (state == null)
            {
                throw new ArgumentException("State not found");
            }

            if (!employee.EmployeeStatenames.Any(es => es.StatenameId == stateId))
            {
                employee.EmployeeStatenames.Add(new EmployeeStatename
                {
                    EmployeeId = employeeId,
                    StatenameId = stateId
                });

                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task RemoveStateFromEmployeeAsync(Guid employeeId, Guid stateId)
        {
            await _unitOfWork.Employees.RemoveStateFromEmployeeAsync(employeeId, stateId);
            await _unitOfWork.CompleteAsync();
        }
    }
}