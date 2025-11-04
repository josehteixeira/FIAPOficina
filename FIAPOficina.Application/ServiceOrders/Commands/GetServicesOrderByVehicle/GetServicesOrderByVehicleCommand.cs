namespace FIAPOficina.Application.ServiceOrders.Commands.GetServicesOrderByVehicle
{
    public record GetServicesOrderByVehicleCommand(string ClientIdentifier, string VehiclePlate);
}