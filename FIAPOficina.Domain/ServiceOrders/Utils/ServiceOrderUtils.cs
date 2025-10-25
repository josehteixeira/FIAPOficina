using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Domain.ServiceOrders.Utils
{
    public static class ServiceOrderUtils
    {
        public static bool ValidQuantity(int value) => value > 0;
        public static bool ValidValue(decimal value) => value > 0;

    }
}
