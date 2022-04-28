using System;
using System.Collections.Generic;

namespace app_computer.Models
{
    public partial class OrderComponent
    {
        public int IdOrder { get; set; }
        public int IdComp { get; set; }
        public int CompCount { get; set; }

        public virtual Component IdCompNavigation { get; set; } = null!;
        public virtual Order IdOrderNavigation { get; set; } = null!;
    }
}
