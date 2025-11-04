using System;

namespace Gestion_Employes.Models
{
    public enum Role
    {
        Employee,
        Manager,
        Intern
    }

    public abstract class Employee
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public Guid? DepartmentId { get; set; }

        protected Employee(string firstName, string lastName, Role role)
        {
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }

        public override string ToString() => $"{FirstName} {LastName} - {Role} - Dept:{DepartmentId?.ToString() ?? "None"} (Id:{Id})";
    }
}
