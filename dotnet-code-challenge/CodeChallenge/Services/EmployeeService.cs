using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;
using Microsoft.AspNetCore.Authentication;

namespace CodeChallenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if(employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }

        public ReportingStructure GetReportingStructure(string id)
        {
            var employee = _employeeRepository.GetById(id);
            if(employee == null){
                return null;
            }

            var reportingStructure = new ReportingStructure
            {
                employee = employee,
                NumberOfReports = this.GetNumberOfDirectReports(employee)
            };
            return reportingStructure;
        }

        // Using recursion we can traverse the tree of employees.        
        private int GetNumberOfDirectReports(Employee employee, int total = 0)
        {
            // If there are no direct reports, the total remains the same
            if(employee.DirectReports == null) {
                return total;
            } else {
                // For each direct report, we recurse, and increment the total by 1
                foreach(Employee directReport in employee.DirectReports) {
                    total = GetNumberOfDirectReports(directReport, total + 1);
                }
                return total;
            }
        }

        public Compensation CreateCompensation(Compensation compensation)
        {
            if(compensation != null)
            {
                _employeeRepository.CreateCompensation(compensation);
                _employeeRepository.SaveCompensationAsync().Wait();
                
            }
            return null;
        }

        public Compensation GetCompensation(string id)
        {
            return _employeeRepository.GetCompensation(id);
        }
    }
}
