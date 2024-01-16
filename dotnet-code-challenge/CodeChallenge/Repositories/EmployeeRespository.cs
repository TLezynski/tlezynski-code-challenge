using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext) //, CompensationContext compensationContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            /**
            For an unknown reason, the code provided doesn't work as intended.
            The "directReports" field is null, as if the seeded data isn't initialized.
            Looping over it manually seems to initialze it. In fact, even looping over it
            an empty loop works.
            */

            foreach(Employee e in _employeeContext.Employees) {}
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }

        public Compensation CreateCompensation(Compensation compensation)
        {
            _employeeContext.Compensations.Add(compensation);
            return compensation;
        }

        public Compensation GetCompensation(String id)
        {
            return _employeeContext.Compensations.SingleOrDefault(c => c.EmployeeId == id);
        }

        public Task SaveCompensationAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }
    }
}
