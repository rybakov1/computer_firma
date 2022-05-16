using System;
using System.Collections.ObjectModel;

namespace app_computer.Logic
{
    public class OrderMem
    {
        public OrderMem()
        {
            Comps = new ObservableCollection<ComponentMem>();
        }
        public int IdOrder { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? TotalPrice { get; set; }
        public ObservableCollection<ComponentMem> Comps { get; set; }
    }
}