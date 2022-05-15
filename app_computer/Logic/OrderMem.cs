using System;

namespace app_computer.Logic
{
    public class OrderMem
    {
        public int IdOrder { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? TotalPrice { get; set; }
        public int? IdComp { get; set; }
        public int? CompCount { get; set; }
    }
}