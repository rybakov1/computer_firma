using System;
using System.Collections.Generic;

namespace app_computer.Models
{
    public partial class WarranityTalon
    {
        public WarranityTalon()
        {
            Orders = new HashSet<Order>();
        }

        public int IdTalon { get; set; }
        public DateOnly? Time { get; set; }
        public int? IdEmployee { get; set; }

        public virtual Employee? IdEmployeeNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
