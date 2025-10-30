namespace FIAPOficina.Api.Models.ServiceOrders.Responses
{
    public record ServiceOrderMaterialResponse
    (
        Guid Id,
        Guid MaterialId,
        Guid ServiceOrderId,
        int Quantity,
        decimal UnitValue
    );
}