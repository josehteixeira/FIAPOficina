namespace FIAPOficina.Api.Models.ServiceOrders.Responses
{
    public record ServiceOrderServiceResponse
    (
        Guid Id,
        Guid ServiceId,
        Guid ServiceOrderId,
        int Quantity,
        decimal UnitValue
    );
}