using FIAPOficina.Api.Helpers;
using FIAPOficina.Api.Models.Auth.Requests;
using FIAPOficina.Application.Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FIAPOficina.Api.Controllers
{
    [ApiController]
    [Route(RoutesHelper.Auth.Controller)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost(RoutesHelper.Auth.Login)]
        [SwaggerOperation(
            Summary = "Login.",
            Description = "Logins in to the system with the provided credentials."
        )]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var loginResult = await _authService.AuthenticateUser(request.Username, request.Password);

            if (loginResult is null)
            {
                return Unauthorized();
            }

            return Ok(loginResult);
        }
    }
}