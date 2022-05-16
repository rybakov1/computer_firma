using System;
using System.Collections.Generic;
using app_computer.Models;

namespace app_computer.Models
{
    public partial class StorageComp
    {
        public int IdStorage { get; set; }
        public int IdComp { get; set; }
        public int? Count { get; set; }

        public virtual Component IdCompNavigation { get; set; } = null!;
        public virtual Storage IdStorageNavigation { get; set; } = null!;
    }
}
