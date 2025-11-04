using System;
using System.Linq;
using Gestion_Employes.Services;
using Gestion_Employes.Models;
using Gestion_Employes.Patterns;
using Gestion_Employes.Repositories;

namespace Gestion_Employes.Views
{
    public class ConsoleView
    {
        private readonly DepartmentService _deptService;
        private readonly EmployeeService _empService;
        private readonly IFilterStrategy _filterStrategy;

        public ConsoleView(DepartmentService deptService, EmployeeService empService, IFilterStrategy filterStrategy)
        {
            _deptService = deptService;
            _empService = empService;
            _filterStrategy = filterStrategy;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n--- Gestion des Employés ---");
                Console.WriteLine("1- Creer un Département");
                Console.WriteLine("2- Lister les Départements");
                Console.WriteLine("3- Creer un Employé");
                Console.WriteLine("4- Lister les Employés");
                Console.WriteLine("5- Filtrer les Employés par Département");
                Console.WriteLine("6- Quitter");
                Console.Write("Choix: ");
                var key = Console.ReadLine();

                switch (key)
                {
                    case "1": CreateDepartment(); break;
                    case "2": ListDepartments(); break;
                    case "3": CreateEmployee(); break;
                    case "4": ListEmployees(); break;
                    case "5": FilterByDepartment(); break;
                    case "6": return;
                    default: Console.WriteLine("Choix invalide"); break;
                }
            }
        }

        private void CreateDepartment()
        {
            Console.Write("Nom du département: ");
            var name = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Nom invalide.");
                return;
            }

            try
            {
                _deptService.CreateDepartment(name);
                Console.WriteLine("Département créé.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}");
            }
        }

        private void ListDepartments()
        {
            var depts = _deptService.ListDepartments().ToList();
            if (!depts.Any()) { Console.WriteLine("Aucun département."); return; }
            foreach (var d in depts) Console.WriteLine($"{d.Id} - {d.Name}");
        }

        private void CreateEmployee()
        {
            Console.Write("Prénom: "); var first = Console.ReadLine() ?? string.Empty;
            Console.Write("Nom: "); var last = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(last))
            {
                Console.WriteLine("Prénom ou nom invalide.");
                return;
            }
            Console.Write("Role (Employee/Manager/Intern): ");
             var roleStr = Console.ReadLine();
            if (!Enum.TryParse<Role>(roleStr, true, out var role)) role = Role.Employee;

            var allDepts = _deptService.ListDepartments().ToList();
            Guid? deptId = null;
            if (allDepts.Any())
            {
                Console.WriteLine("Départements disponibles:");
                for (int i = 0; i < allDepts.Count; i++) Console.WriteLine($"{i+1}- {allDepts[i].Name}");
                Console.Write("Sélectionnez (numéro) ou vide: ");
                var sel = Console.ReadLine();
                if (int.TryParse(sel, out var idx) && idx >= 1 && idx <= allDepts.Count) deptId = allDepts[idx-1].Id;
            }

            var emp = Patterns.EmployeeFactory.Create(first, last, role);
            try
            {
                _empService.CreateEmployee(emp, deptId);
                Console.WriteLine("Employé créé.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}");
            }
        }

        private void ListEmployees()
        {
            var emps = _empService.ListEmployees().ToList();
            if (!emps.Any()) { Console.WriteLine("Aucun employé."); return; }
            foreach (var e in emps) Console.WriteLine(e);
        }

        private void FilterByDepartment()
        {
            var depts = _deptService.ListDepartments().ToList();
            if (!depts.Any()) { Console.WriteLine("Aucun département."); return; }
            for (int i = 0; i < depts.Count; i++) Console.WriteLine($"{i+1}- {depts[i].Name}");
            Console.Write("Sélectionnez (numéro): ");
            var sel = Console.ReadLine();
            if (!int.TryParse(sel, out var idx) || idx < 1 || idx > depts.Count) { Console.WriteLine("Selection invalide"); return; }
            var deptId = depts[idx-1].Id;
            var filtered = _filterStrategy.Filter(_empService.ListEmployees(), deptId);
            foreach (var e in filtered) Console.WriteLine(e);
        }
    }
}
