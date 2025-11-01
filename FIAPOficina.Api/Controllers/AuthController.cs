using FIAPOficina.Api.Models.Auth.Requests;
using FIAPOficina.Application.Authentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace FIAPOficina.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
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