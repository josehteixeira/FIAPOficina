namespace FIAPOficina.Application.ServiceOrders.Commands.GetAllServiceOrders
{
    public record GetAllServiceOrdersCommand(Guid? VehicleId = null, Guid? ClientId = null);
}