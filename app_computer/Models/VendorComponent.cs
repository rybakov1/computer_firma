using System;
using System.Collections.Generic;
using app_computer.Models;

namespace app_computer.Models
{
    public partial class VendorComponent
    {
        public int IdVendor { get; set; }
        public int IdComp { get; set; }
        public int Count { get; set; }
        public DateTime OrderTime { get; set; }

        public virtual Component IdCompNavigation { get; set; } = null!;
        public virtual Vendor IdVendorNavigation { get; set; } = null!;
    }
}
