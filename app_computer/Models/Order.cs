using System;
using System.Collections.Generic;

namespace app_computer.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderComponents = new HashSet<OrderComponent>();
            IdServices = new HashSet<Service>();
        }

        public int IdOrder { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RealizationDate { get; set; }
        public int? IdCustomer { get; set; }
        public int? Prepayment { get; set; }
        public sbyte? PaymentMark { get; set; }
        public int? TotalPrice { get; set; }
        public int? IdEmp { get; set; }
        public int? IdTalon { get; set; }

        public virtual Customer? IdCustomerNavigation { get; set; }
        public virtual Employee? IdEmpNavigation { get; set; }
        public virtual WarranityTalon? IdTalonNavigation { get; set; }
        public virtual ICollection<OrderComponent> OrderComponents { get; set; }

        public virtual ICollection<Service> IdServices { get; set; }
    }
}
