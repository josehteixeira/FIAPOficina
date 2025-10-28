using System.ComponentModel.DataAnnotations;

namespace FIAPOficina.Api.Models.Materials.Responses
{
    public record MaterialResponse(
        [Required] Guid Id,
        [Required] string Name,
        [Required] string Description,
        [Required] string Brand,
        [Required] decimal Value,
        [Required] int Quantity);
}
