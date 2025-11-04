using System;
using System.Collections.Generic;
using Gestion_Employes.Models;
using Gestion_Employes.Repositories;

namespace Gestion_Employes.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository _repo;

        public DepartmentService(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Department> ListDepartments() => _repo.GetAll();

        public void CreateDepartment(string name)
        {
            var d = new Department(name);
            _repo.Add(d);
        }
    }
}
