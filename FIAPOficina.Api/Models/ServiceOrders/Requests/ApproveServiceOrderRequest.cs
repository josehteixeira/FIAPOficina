using System.ComponentModel.DataAnnotations;

namespace FIAPOficina.Api.Models.ServiceOrders.Requests
{
    public record ApproveServiceOrderRequest(
        [Required] string ClientIdentifier,
        [Required] string VehiclePlate);
}