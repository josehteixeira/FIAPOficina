using FIAPOficina.Domain.ServiceOrders.Entities;

namespace FIAPOficina.Application.ServiceOrders.Commands.UpdateServiceOrder
{
    public record UpdateServiceOrderCommand(
        Guid Id,
        Guid VehicleId,
        ServiceOrderStatus Status,
        List<ServiceOrderServiceToUpdate> Services,
        List<ServiceOrderMaterialToUpdate> Materials);
}