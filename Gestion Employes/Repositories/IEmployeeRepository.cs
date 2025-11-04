using System;
using System.Collections.Generic;
using Gestion_Employes.Models;

namespace Gestion_Employes.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        void Add(Employee employee);
    Employee? GetById(Guid id);
        IEnumerable<Employee> GetByDepartment(Guid departmentId);
    }
}
