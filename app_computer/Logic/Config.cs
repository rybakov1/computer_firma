using System.Collections.Generic;

namespace app_computer.Logic
{
    public static class Config
    {
        public static Dictionary<int, int> Id = new();
        public static int? IdCustomer;

        public static bool IsAuthorized = false;
        public static bool IsAdmin = false;

        public static int? PaymentDiscount = 0;
        public static int? DeliveryDiscount = 0;
    }
}
