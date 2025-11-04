using System;
using System.Collections.Generic;
using Gestion_Employes.Models;

namespace Gestion_Employes.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        void Add(Department department);
    Department? GetById(Guid id);
    }
}
