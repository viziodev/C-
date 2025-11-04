using System;

namespace Gestion_Employes.Models
{
    public class GenericEmployee : Employee
    {
        public GenericEmployee(string firstName, string lastName, Role role) : base(firstName, lastName, role)
        {
        }
    }
}
