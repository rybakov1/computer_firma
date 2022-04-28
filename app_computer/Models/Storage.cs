using System;
using System.Collections.Generic;

namespace app_computer.Models
{
    public partial class Storage
    {
        public Storage()
        {
            StorageComps = new HashSet<StorageComp>();
        }

        public int IdStorage { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<StorageComp> StorageComps { get; set; }
    }
}
