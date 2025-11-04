using Gestion_Employes.Models;

namespace Gestion_Employes.Patterns
{
    public static class EmployeeFactory
    {
        public static Employee Create(string firstName, string lastName, Role role)
        {
            return role switch
            {
                Role.Manager => new Manager(firstName, lastName),
                _ => new GenericEmployee(firstName, lastName, role),
            };
        }
    }
}
