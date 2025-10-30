using System.ComponentModel;

namespace FIAPOficina.Api.Models.ServiceOrders.Requests
{
    public record ServiceOrderMaterialRequest
    (
        [DefaultValue(null)] Guid? Id,
        Guid MaterialId,
        int Quantity
    );
}