namespace FIAPOficina.Application.ServiceOrders.Commands.UpdateServiceOrder
{
    public record UpdateServiceOrderCommand(
        Guid Id,
        Guid VehicleId,
        List<ServiceOrderServiceToUpdate> Services,
        List<ServiceOrderMaterialToUpdate> Materials);
}