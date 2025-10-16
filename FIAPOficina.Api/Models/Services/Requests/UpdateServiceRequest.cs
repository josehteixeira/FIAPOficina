using System.ComponentModel.DataAnnotations;

namespace FIAPOficina.Api.Models.Services.Requests
{
    public record UpdateServiceRequest
    (
        [Required] string Name,
        [Required] string Description,
        [Required] double Value
    );
}
