namespace FIAPOficina.Application.ServiceOrders.Commands.CreateServiceOrder
{
    public record CreateServiceOrderCommand(
        Guid VehicleId,
        List<ServiceOrderServiceToCreate> Services,
        List<ServiceOrderMaterialToCreate> Materials);
}