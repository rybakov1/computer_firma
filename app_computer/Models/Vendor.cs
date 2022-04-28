using System;
using System.Collections.Generic;

namespace app_computer.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            VendorComponents = new HashSet<VendorComponent>();
        }

        public int IdVendor { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<VendorComponent> VendorComponents { get; set; }
    }
}
