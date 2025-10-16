using System.ComponentModel.DataAnnotations;

namespace FIAPOficina.Api.Models.Users.Requests
{
    public record CreateUserRequest(
        [Required] string Name,
        [Required] string UserName,
        [Required] string Password
    );
}