using System;
using System.Collections.Generic;

namespace app_computer.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int IdCustomer { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public string? Middlename { get; set; }
        public string? Address { get; set; }
        public string? MobileNumber { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
