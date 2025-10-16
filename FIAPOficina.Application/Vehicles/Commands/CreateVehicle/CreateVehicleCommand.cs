namespace FIAPOficina.Application.Vehicles.Commands.CreateVehicle
{
    public record CreateVehicleCommand(string Brand, string Model, int Year, string Plate, string Color, Guid ClientId);
}