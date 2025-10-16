namespace FIAPOficina.Application.Vehicles.Commands.UpdateVehicle
{
    public record UpdateVehicleCommand(Guid Id, string Brand, string Model, int Year, string Plate, string Color);
}