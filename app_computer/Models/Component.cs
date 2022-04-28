using System;
using System.Collections.Generic;

namespace app_computer.Models
{
    public partial class Component
    {
        public Component()
        {
            OrderComponents = new HashSet<OrderComponent>();
            StorageComps = new HashSet<StorageComp>();
            VendorComponents = new HashSet<VendorComponent>();
            IdPresets = new HashSet<Preset>();
        }

        public int IdComp { get; set; }
        public int? IdType { get; set; }
        public string? Model { get; set; }
        public string? Company { get; set; }
        public string? Country { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string? Specifications { get; set; }
        /// <summary>
        /// month
        /// </summary>
        public int? Warranty { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }

        public virtual Typeofcomponent? IdTypeNavigation { get; set; }
        public virtual ICollection<OrderComponent> OrderComponents { get; set; }
        public virtual ICollection<StorageComp> StorageComps { get; set; }
        public virtual ICollection<VendorComponent> VendorComponents { get; set; }

        public virtual ICollection<Preset> IdPresets { get; set; }
    }
}
