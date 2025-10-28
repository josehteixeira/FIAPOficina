using System.ComponentModel.DataAnnotations;

namespace FIAPOficina.Api.Models.Materials.Requests
{
    public record UpdateMaterialRequest(
        [Required] string Name,
        [Required] string Description,
        [Required] string Brand,
        [Required] decimal Value,
        [Required] int Quantity);
}