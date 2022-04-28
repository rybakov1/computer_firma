using System;
using System.Collections.Generic;

namespace app_computer.Models
{
    public partial class Service
    {
        public Service()
        {
            IdEmployees = new HashSet<Employee>();
            IdOrders = new HashSet<Order>();
        }

        public int IdService { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }

        public virtual ICollection<Employee> IdEmployees { get; set; }
        public virtual ICollection<Order> IdOrders { get; set; }
    }
}
