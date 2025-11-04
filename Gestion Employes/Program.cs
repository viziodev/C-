using Gestion_Employes.Repositories;
using Gestion_Employes.Services;
using Gestion_Employes.Views;
using Gestion_Employes.Patterns;

// Simple bootstrap
var deptRepo = DepartmentRepository.Instance;
var empRepo = EmployeeRepository.Instance;

var deptService = new DepartmentService(deptRepo);
var empService = new EmployeeService(empRepo, deptRepo);
var filterStrategy = new DepartmentFilterStrategy();

var view = new ConsoleView(deptService, empService, filterStrategy);
view.Run();
