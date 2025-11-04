using System;
using System.Collections.Generic;
using Gestion_Employes.Models;
using Gestion_Employes.Repositories;

namespace Gestion_Employes.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repo;
        private readonly IDepartmentRepository _deptRepo;

        public EmployeeService(IEmployeeRepository repo, IDepartmentRepository deptRepo)
        {
            _repo = repo;
            _deptRepo = deptRepo;
        }

        public IEnumerable<Employee> ListEmployees() => _repo.GetAll();

        public void CreateEmployee(Employee employee, Guid? departmentId)
        {
            if (departmentId.HasValue && _deptRepo.GetById(departmentId.Value) == null)
                throw new InvalidOperationException("Department not found");

            employee.DepartmentId = departmentId;
            _repo.Add(employee);
        }

        public IEnumerable<Employee> FilterByDepartment(Guid departmentId) => _repo.GetByDepartment(departmentId);
    }
}
