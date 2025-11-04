using System;

namespace Gestion_Employes.Models
{
    public class Manager : Employee
    {
        public Manager(string firstName, string lastName) : base(firstName, lastName, Role.Manager)
        {
        }
    }
}
