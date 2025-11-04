using System;
using System.Collections.Generic;
using System.Linq;
using Gestion_Employes.Models;

namespace Gestion_Employes.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static readonly Lazy<EmployeeRepository> _instance = new(() => new EmployeeRepository());
        public static EmployeeRepository Instance => _instance.Value;

        private readonly Dictionary<Guid, Employee> _employees = new();

        private EmployeeRepository() { }

        public IEnumerable<Employee> GetAll() => _employees.Values.ToList().AsReadOnly();

        public void Add(Employee employee)
        {
            if (_employees.ContainsKey(employee.Id))
                throw new InvalidOperationException("Employee already exists");
            _employees[employee.Id] = employee;
        }

    public Employee? GetById(Guid id) => _employees.TryGetValue(id, out var e) ? e : null;

        public IEnumerable<Employee> GetByDepartment(Guid departmentId) => _employees.Values.Where(e => e.DepartmentId == departmentId).ToList().AsReadOnly();
    }
}
