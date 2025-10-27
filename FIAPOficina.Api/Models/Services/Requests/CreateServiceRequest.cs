using System.ComponentModel.DataAnnotations;

namespace FIAPOficina.Api.Models.Services.Requests
{
    public record CreateServiceRequest
    (
        [Required] string Name,
        [Required] string Description,
        [Required] decimal Value
    );
}
