namespace FIAPOficina.Api.Models.ServiceOrders.Requests
{
    public record ServiceOrderRequest
    (
        Guid VehicleId,
        ServiceOrderServiceRequest[] Services,
        ServiceOrderMaterialRequest[] Materials,
        int Status
    );
}