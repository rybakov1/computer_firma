using System;

namespace app_computer.Logic
{
    public class ComponentMem
    {
        public int Id { get; set; }
        public string? Model { get; set;}
        public string? Title { get; set; }
        public string? Company { get; set; }
        public string? Country { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public int? Price {get; set;}
        public string? Description { get; set;}
        public string? Specifications { get; set;}
        public int? Warranty { get; set;}
        public int? Count { get; set;}
        public string? TotalPrice { get; set;}
    }
}
