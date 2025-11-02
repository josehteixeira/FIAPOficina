namespace FIAPOficina.Application.ServiceOrders.Commands.RejectServiceOrder
{
    public record RejectServiceOrderCommand(Guid ServiceOrderId, string ClientIdentifier, string VehiclePlate);
}