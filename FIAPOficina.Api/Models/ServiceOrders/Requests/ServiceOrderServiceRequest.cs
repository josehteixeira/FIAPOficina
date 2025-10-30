using System.ComponentModel;

namespace FIAPOficina.Api.Models.ServiceOrders.Requests
{
    public record ServiceOrderServiceRequest
    (
        [DefaultValue(null)] Guid? Id,
        Guid ServiceId,
        int Quantity
    );
}
