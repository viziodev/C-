using System;
using System.Collections.Generic;
using System.Linq;
using Gestion_Employes.Models;

namespace Gestion_Employes.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private static readonly Lazy<DepartmentRepository> _instance = new(() => new DepartmentRepository());
        public static DepartmentRepository Instance => _instance.Value;

        private readonly List<Department> _departments = new();

        private DepartmentRepository() { }

        public IEnumerable<Department> GetAll() => _departments.AsReadOnly();

        public void Add(Department department)
        {
            if (_departments.Any(d => d.Name.Equals(department.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Department already exists");
            _departments.Add(department);
        }

    public Department? GetById(Guid id) => _departments.FirstOrDefault(d => d.Id == id);
    }
}
