using System.ComponentModel.DataAnnotations;

namespace FIAPOficina.Api.Models.Clients.Requests
{
    public record CreateClientRequest(
        [Required] string Name,
        [Required] string Identifier,
        [Required] string Phone,
        [Required] string Email,
        [Required] string Address
    );
}
