using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Domain.ServiceOrders.Utils
{
    public static class ServiceOrderUtils
    {
        public static int ValidQuantity(int value) {
            if (value > 0)
                return value;
            throw new ArgumentOutOfRangeException("Quantity");
        }
        public static decimal ValidValue(decimal value)
        {
            if (value > 0)
                return value;
            throw new ArgumentOutOfRangeException("Value");
        }

    }
}
