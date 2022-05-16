using System;
using System.Collections.Generic;

namespace app_computer.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
            WarranityTalons = new HashSet<WarranityTalon>();
            IdServices = new HashSet<Service>();
        }

        public int IdEmployee { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public string? Middlename { get; set; }
        public string? Sex { get; set; }
        public string? Address { get; set; }
        public string MobileNumber { get; set; } = null!;
        public string? Passport { get; set; }
        public int? IdPos { get; set; }
        public DateOnly? BirthdayDate { get; set; }
        public string? Password { get; set; }

        public virtual Position? IdPosNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<WarranityTalon> WarranityTalons { get; set; }

        public virtual ICollection<Service> IdServices { get; set; }
    }
}
