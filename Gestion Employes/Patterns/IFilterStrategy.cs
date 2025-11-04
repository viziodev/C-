using System;
using System.Collections.Generic;
using Gestion_Employes.Models;

namespace Gestion_Employes.Patterns
{
    public interface IFilterStrategy
    {
        IEnumerable<Employee> Filter(IEnumerable<Employee> employees, Guid? departmentId);
    }
}
