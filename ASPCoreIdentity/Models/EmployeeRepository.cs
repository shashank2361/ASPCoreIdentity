using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreIdentity.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int Id);
        IEnumerable<Employee> GetAllEmployees();
        Employee Add(Employee employee);
        Employee Update(Employee employee);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CompanyDBContext companyDBContext;

        public EmployeeRepository( CompanyDBContext CompanyDBContext)
        {
            companyDBContext = CompanyDBContext;
        }

        public Employee Add(Employee employee)
        {
            companyDBContext.Add(employee);
            return employee;     
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
           return companyDBContext.Employees ;            
        }

        public Employee GetEmployee(int Id)
        {
            return companyDBContext.Employees.Find(Id);
        }

        public Employee Update(Employee employee)
        {
            companyDBContext.Update<Employee>(employee);
            companyDBContext.SaveChanges();
            return employee;
        }
    }
}
