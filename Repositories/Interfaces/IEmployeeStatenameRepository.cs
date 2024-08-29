using Enwage_API.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Enwage_API.Repositories.Interfaces
{
    public interface IEmployeeStatenameRepository
{
    Task<IEnumerable<EmployeeStatename>> FindAsync(Expression<Func<EmployeeStatename, bool>> predicate);
    Task<EmployeeStatename> AddEmployeeState(EmployeeStatename employeeStatename);
    void Remove(EmployeeStatename entity);
}
}