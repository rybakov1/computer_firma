using System;
using System.Collections.Generic;
using app_computer.Models;

namespace app_computer.Models
{
    public partial class Typeofcomponent
    {
        public Typeofcomponent()
        {
            Components = new HashSet<Component>();
        }

        public int IdType { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Component> Components { get; set; }
    }
}
