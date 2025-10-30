namespace FIAPOficina.Domain.Vehicles.Entities
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Plate { get; set; }
        public string Color { get; set; }
        public Guid ClientId { get; set; }

        public Vehicle(string brand, string model, int year, string plate, string color, Guid clientId, Guid? id = null)
        {
            if (!Utils.VehicleUtils.IsPlateValid(plate.ToUpper()))
            {
                throw new ArgumentException("Invalid plate", nameof(Plate));
            }

            Id = id.HasValue ? id.Value : Guid.NewGuid();
            Brand = brand;
            Model = model;
            Year = year;
            Plate = plate;
            Color = color;
            ClientId = clientId;
        }

        public Vehicle(Vehicle vehicle, Guid id)
        {
            if (!Utils.VehicleUtils.IsPlateValid(vehicle.Plate.ToUpper()))
            {
                throw new ArgumentException("Invalid plate", nameof(Plate));
            }

            Id = id;
            Brand = vehicle.Brand;
            Model = vehicle.Model;
            Year = vehicle.Year;
            Plate = vehicle.Plate;
            Color = vehicle.Color;
            ClientId = vehicle.ClientId;
        }
    }
}