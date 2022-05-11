using System;
using System.Collections.Generic;

namespace app_computer.Models
{
    public partial class Preset
    {
        public Preset()
        {
            Comps = new HashSet<Component>();
        }

        public int IdPreset { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Component> Comps { get; set; }
    }
}
