using Enwage_API.DTOs;
using Enwage_API.Models;
using Enwage_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Enwage_API.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(Enwage2Context context) : base(context) { }



        // method to check emaail 
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Employees.AnyAsync(e => e.Email == email);
        }

        //public async Task<PaginatedResult<Employee>> GetPaginatedAsync(int pageNumber, int pageSize, string searchQuery)
        //{
        //    //var query = _context.Employees
        //    //  .Include(x=>x.Client).AsQueryable();
        //    var query = _context.Employees
        //      .Include(x => x.Client)
        //      .Include(x => x.EmployeeStatenames)
        //      .ThenInclude(x => x.Statename)
        //      .AsQueryable();


        //    if (!string.IsNullOrWhiteSpace(searchQuery))
        //    {
        //        query = query.Where(e => e.Name.ToLower().Contains(searchQuery.ToLower()) || e.Email.ToLower().Contains(searchQuery.ToLower()));
        //    }

        //    var totalCount = await query.CountAsync();
        //    var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        //    var items = await query
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();


        //    return new PaginatedResult<Employee>
        //    {
        //        TotalCount = totalCount,
        //        PageSize = pageSize,
        //        CurrentPage = pageNumber,
        //        TotalPages = totalPages,
        //        Items = items
        //    };
        //}

        public async Task<PaginatedResult<EmployeeDto>> GetPaginatedAsync(int pageNumber, int pageSize, string searchQuery)
        {
            var query = _context.Employees
                .Include(x => x.Client)
                .Include(x => x.EmployeeStatenames)
                .ThenInclude(x => x.Statename)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(e => e.Name.ToLower().Contains(searchQuery.ToLower()) || e.Email.ToLower().Contains(searchQuery.ToLower()));
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id, 
                    ClientId = e.Client.Id,
                    ClientName = e.Client.Name,
                    ChangeState = e.Changestate, 
                    ExperienceStartDate = e.Experiencestartdate, 
                    ExperienceEndDate = e.Experienceenddate, 
                    HourlyRate = e.Hourlyrate, 
                    IsPresent = e.Ispresent, 
                    DateOfBirth = e.Dateofbirth,
                    Name = e.Name,
                    Email = e.Email,
                    Gender = e.Gender, 
                    States = e.EmployeeStatenames.Select(es => es.StatenameId).ToList()
                })
                .ToListAsync();

            return new PaginatedResult<EmployeeDto>
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = totalPages,
                Items = items
            };
        }


        public async Task<Employee> GetEmployeeWithStatesAsync(Guid id)
        {
            return await _context.Employees
                .Include(e => e.Client)
                .Include(e => e.EmployeeStatenames)
                    .ThenInclude(es => es.Statename)
                .FirstOrDefaultAsync(w => w.Id == w.Id);
        }

        public async Task AddStateToEmployeeAsync(Guid employeeId, Guid stateId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            var state = await _context.Statenames.FindAsync(stateId);

            if (employee != null && state != null)
            {
                employee.EmployeeStatenames.Add(new EmployeeStatename
                {
                    EmployeeId = employeeId,
                    StatenameId = stateId
                });
            }
        }

        public async Task RemoveStateFromEmployeeAsync(Guid employeeId, Guid stateId)
        {
            var employeeStatename = await _context.EmployeeStatenames
                .FirstOrDefaultAsync(es => es.EmployeeId == employeeId && es.StatenameId == stateId);

            if (employeeStatename != null)
            {
                _context.EmployeeStatenames.Remove(employeeStatename);
            }
        }


        public async Task<Employee> GetEmployeeWithClientAsync(Guid id)
        {
            return await _context.Employees
                .Include(e => e.Client)
                .Include(e => e.EmployeeStatenames)
                .ThenInclude(es => es.Statename)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}







