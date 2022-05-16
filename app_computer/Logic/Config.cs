using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_computer.Logic
{
    public static class Config
    {
        public static Dictionary<int, int> Id = new();
        public static bool IsAuthorized = false;
        public static bool IsAdmin = false;
        public static int? IdCustomer;
    }
}
