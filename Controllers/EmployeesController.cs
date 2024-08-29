using Enwage_API.DTOs;
using Enwage_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Enwage_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<EmployeeDto>>> GetEmployees(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string searchQuery = null)
        {
            var result = await _employeeService.GetPaginatedEmployeesAsync(pageNumber, pageSize, searchQuery);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(Guid id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto employeeDto)
        {
            var createdEmployee = await _employeeService.CreateEmployeeAsync(employeeDto);
            return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.Id }, createdEmployee);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeDto employeeDto)
        //{
        //    var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employeeDto);
        //    return Ok(updatedEmployee);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employeeDto);
            return Ok(updatedEmployee);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpPost("{employeeId}/states/{stateId}")]
        public async Task<IActionResult> AddStateToEmployee(Guid employeeId, Guid stateId)
        {
            try
            {
                await _employeeService.AddStateToEmployeeAsync(employeeId, stateId);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{employeeId}/states/{stateId}")]
        public async Task<IActionResult> RemoveStateFromEmployee(Guid employeeId, Guid stateId)
        {
            try
            {
                await _employeeService.RemoveStateFromEmployeeAsync(employeeId, stateId);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}