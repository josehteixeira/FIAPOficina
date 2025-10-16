using System.ComponentModel.DataAnnotations;

namespace FIAPOficina.Api.Models.Users.Requests
{
    public record UpdateUserRequest(
        [Required] string Name,
        [Required] string UserName
    );
}