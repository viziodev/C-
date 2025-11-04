using System;
using System.Collections.Generic;

namespace Gestion_Employes.Models
{
    public class Department
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public Department(string name)
        {
            Name = name;
        }

        public override string ToString() => $"{Name} ({Id})";
    }
}
