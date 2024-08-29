using Enwage_API.Models;
using Enwage_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Enwage_API.Repositories
{
    public class EmployeeStatenameRepository : IEmployeeStatenameRepository
    {
        private readonly Enwage2Context _context;

        public EmployeeStatenameRepository(Enwage2Context context)
        {
            _context = context;
        }

        public async Task<EmployeeStatename> AddEmployeeState(EmployeeStatename employeeStatename)
        {
            if (employeeStatename == null)
            {
                throw new ArgumentNullException(nameof(employeeStatename));
            }

            _context.EmployeeStatenames.Add(employeeStatename);
            await _context.SaveChangesAsync();
            return employeeStatename;
        }

        public async Task<IEnumerable<EmployeeStatename>> FindAsync(Expression<Func<EmployeeStatename, bool>> predicate)
        {
            return await _context.EmployeeStatenames.Where(predicate).ToListAsync();
        }

        public void Remove(EmployeeStatename entity)
        {
            _context.EmployeeStatenames.Remove(entity);
        }
    }
}