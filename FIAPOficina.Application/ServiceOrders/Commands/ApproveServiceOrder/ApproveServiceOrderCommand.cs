namespace FIAPOficina.Application.ServiceOrders.Commands.ApproveServiceOrder
{
    public record ApproveServiceOrderCommand(Guid ServiceOrderId, string ClientIdentifier, string VehiclePlate);
}