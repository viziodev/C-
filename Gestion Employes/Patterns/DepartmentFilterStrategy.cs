using System;
using System.Collections.Generic;
using System.Linq;
using Gestion_Employes.Models;

namespace Gestion_Employes.Patterns
{
    public class DepartmentFilterStrategy : IFilterStrategy
    {
        public IEnumerable<Employee> Filter(IEnumerable<Employee> employees, Guid? departmentId)
        {
            if (!departmentId.HasValue) return employees;
            return employees.Where(e => e.DepartmentId == departmentId.Value).ToList().AsReadOnly();
        }
    }
}
