using System.ComponentModel.DataAnnotations;

namespace FIAPOficina.Api.Models.ServiceOrders.Requests
{
    public record RejectServiceOrderRequest(
        [Required] string ClientIdentifier,
        [Required] string VehiclePlate);
}