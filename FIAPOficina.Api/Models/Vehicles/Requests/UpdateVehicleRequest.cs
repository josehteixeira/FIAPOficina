using System.ComponentModel.DataAnnotations;

namespace FIAPOficina.Api.Models.Vehicles.Requests
{
    public record UpdateVehicleRequest
    (
        [Required] string Brand,
        [Required] string Model,
        [Required] int Year,
        [Required] string Plate,
        [Required] string Color,
        [Required] Guid ClientId
    );
}
