namespace FIAPOficina.Domain.Utils
{
    public static class UtilsCommon
    {
        public static int ValidQuantity(int value)
        {
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
