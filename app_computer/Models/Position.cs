using System;
using System.Collections.Generic;

namespace app_computer.Models
{
    public partial class Position
    {
        public Position()
        {
            Employees = new HashSet<Employee>();
        }

        public int IdPos { get; set; }
        public string? PostName { get; set; }
        public int? Salary { get; set; }
        public string? Responsibilities { get; set; }
        public string? Requirements { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
