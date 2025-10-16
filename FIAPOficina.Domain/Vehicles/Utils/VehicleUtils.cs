using System.Text.RegularExpressions;

namespace FIAPOficina.Domain.Vehicles.Utils
{
    internal static class VehicleUtils
    {
        private static readonly Regex OldPattern = new Regex(@"^[A-Z]{3}-?[0-9]{4}$", RegexOptions.Compiled, TimeSpan.FromSeconds(1));
        private static readonly Regex MercosulPattern = new Regex(@"^[A-Z]{3}[0-9][A-Z][0-9]{2}$", RegexOptions.Compiled, TimeSpan.FromSeconds(1));

        public static bool IsPlateValid(string plate)
        {
            return OldPattern.IsMatch(plate) || MercosulPattern.IsMatch(plate);
        }
    }
}