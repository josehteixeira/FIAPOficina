namespace FIAPOficina.Domain.Vehicles.Entities
{
    public class Vehicle
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Plate { get; set; }
        public string Color { get; set; }
        public Guid ClientId { get; set; }

        public Vehicle(string brand, string model, int year, string plate, string color, Guid clientId)
        {
            if (!Utils.VehicleUtils.IsPlateValid(plate))
            {
                throw new ArgumentException("Invalid plate", nameof(Plate));
            }

            Brand = brand;
            Model = model;
            Year = year;
            Plate = plate;
            Color = color;
            ClientId = clientId;
        }
    }
}